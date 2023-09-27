using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService: IRoleService
    {
        public readonly IRoleRespository _roleRespository;
        public RoleService(IRoleRespository roleRespository)
        {
            _roleRespository = roleRespository;
        }

        public Task<List<Role>> GetRoles()
        {
            return Task.Run(() => _roleRespository.GetRoles());
        }

        public Task<Role> AddRole(string roleName)
        {
            return Task.Run(() =>
            {
                Role role = new() { ID = _roleRespository.GetRoles().Count + 1, Name = roleName };
                Role model = new();

                _roleRespository.AddRole(role);
                model = role;

                return model;
            });
        }

        public Task<string> UpdateRole(Role role)
        {
            return Task.Run(() => _roleRespository.UpdateRole(role));
        }

        public Task<bool> DeleteRole(int id)
        {
            return Task.Run(() => _roleRespository.DeleteRole(id));
        }
    }
}
