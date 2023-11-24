using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Models;
using Services;
using System.Data;

namespace CBMCenterApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IStationService _stationService;

        public UserController(IUserService userService, IRoleService roleService, IStationService stationService)
        {
            _userService = userService;
            _roleService = roleService;
            _stationService = stationService;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetUsers([FromForm] string? name)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                name = "";
            }

            var users = await _userService.GetUsers(a => a.Name.Contains(name) || a.PersonName.Contains(name));
            if (users == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = users;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetRoles()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var roles = await _roleService.GetRoles();
            if (roles == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = roles;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddUser([FromForm] User user, [FromForm] List<int> stationIDs)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _userService.AddUser(user);
            if (result == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                List<UserStation> userStations = new();
                stationIDs.ForEach(e =>
                {
                    userStations.Add(new UserStation { UserID = user.ID, StationID = e });
                });
                switch (await _userService.AddUserStation(userStations))
                {
                    case "OtherError":
                        rtn["MSG"] = "OtherError";
                        rtn["Code"] = "400";
                        break;
                    case "OK":
                        rtn["MSG"] = "OK";
                        rtn["Code"] = "200";
                        break;
                }
            }
            rtn["Data"] = result;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddRole([FromForm] string roleName)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var roles = await _roleService.AddRole(roleName);
            if (roles == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = roles;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateUser([FromForm] User user, [FromForm] List<int> stationIDs)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var oldUser = (await _userService.GetUsers(a => a.ID == user.ID)).FirstOrDefault();
            if (oldUser is not { ID: > 0 })
            {
                rtn["MSG"] = "用户不存在或已被删除";
                rtn["Code"] = "500";
                return rtn;
            }

            try
            {
                oldUser.Name = user.Name;
                oldUser.Password = user.Password;
                oldUser.PersonName = user.PersonName;
                oldUser.ContactNumber = user.ContactNumber;
                oldUser.RoleID = user.RoleID;

                var userStations = await _userService.GetUserStations(d => d.UserID == oldUser.ID);
                if (userStations.Any())
                {
                    foreach (var userStation in userStations)
                    {
                        var isAllDeleted = await _userService.DeleteUserStationBy(x => x.ID == userStation.ID);
                        if (!isAllDeleted)
                        {
                            rtn["MSG"] = "服务器更新异常";
                            rtn["Code"] = "500";
                            return rtn;
                        }
                    }
                }

                // 然后再执行添加操作
                if (stationIDs.Count > 0)
                {
                    var userStationsAdd = new List<UserStation>();
                    stationIDs.ForEach(e => { userStationsAdd.Add(new UserStation { UserID = oldUser.ID, StationID = e }); });

                    await _userService.AddUserStation(userStationsAdd);
                }

                switch (await _userService.UpdateUser(oldUser))
                {
                    case "OtherError":
                        rtn["MSG"] = "OtherError";
                        rtn["Code"] = "400";
                        break;
                    case "OK":
                        rtn["MSG"] = "OK";
                        rtn["Code"] = "200";
                        break;
                }
            }
            catch (Exception)
            {

            }

            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateRole([FromForm] Role role)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var roles = await _roleService.UpdateRole(role);
            if (roles == "OtherError")
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = roles;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteUser([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            if (id > 0)
            {
                var isAllDeleted = await _userService.DeleteUserStationBy(a => a.UserID == id);
                if (!isAllDeleted)
                {
                    rtn["MSG"] = "服务器删除异常";
                    rtn["Code"] = "500";
                    return rtn;
                }

                if (await _userService.DeleteUser(id))
                {
                    rtn["MSG"] = "OK";
                    rtn["Code"] = "200";
                }
                else
                {
                    rtn["MSG"] = "OtherError";
                    rtn["Code"] = "400";
                }
            }

            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteRole([FromForm] int rid)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            if (rid > 0)
            {
                var role = (await _roleService.GetRoles()).FirstOrDefault(x => x.ID == rid);
                if (role != null)
                {
                    if (await _roleService.DeleteRole(rid))
                    {
                        rtn["MSG"] = "OK";
                        rtn["Code"] = "200";
                    }
                    else
                    {
                        rtn["MSG"] = "OtherError";
                        rtn["Code"] = "400";
                    }
                }
            }

            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetStations()
        {
            Dictionary<string, object> rtn = new();
            var stations = await _stationService.GetStations();
            if (stations == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
                rtn["Data"] = stations.Select(s => new { s.ID, s.Name, s.AbbrName });
            }
            return rtn;
        }
    }
}
