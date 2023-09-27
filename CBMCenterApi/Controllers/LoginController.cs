using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace CBMCenterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> Login([FromForm] string name = "", [FromForm] string pass = "")
        {
            Dictionary<string, object> rtn = new();
            User user = new();
            UserLogRecord userLogRecord = new()
            {
                UserName = name,
                DateTime = System.DateTime.Now
            };
            switch (_userService.Login(ref user, name, pass))
            {
                case "DatabaseError":
                    {
                        rtn["Code"] = "500";
                        rtn["MSG"] = "数据库读写错误";
                        break;
                    }
                case "LogFailed":
                    {
                        rtn["Code"] = "500";
                        rtn["MSG"] = "用户名或密码不正确";
                        await _userService.AddUserLogRecord(userLogRecord);
                        break;
                    }
                case "NoSuchUser":
                    {
                        rtn["Code"] = "500";
                        rtn["MSG"] = "系统中无此用户";
                        await _userService.AddUserLogRecord(userLogRecord);
                        break;
                    }
                default:
                    {
                        rtn["Code"] = "200";
                        rtn["MSG"] = "登录成功";
                        rtn["Data"] = user;
                        await _userService.AddUserLogRecord(userLogRecord);
                        break;
                    }
            }
            return rtn;
        }
    }
}
