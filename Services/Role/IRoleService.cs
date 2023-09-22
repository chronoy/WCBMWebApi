using Models;

namespace Services
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
        public Task<Role> AddRole(string roleName);
        public Task<string> UpdateRole(Role role);
        public Task<bool> DeleteRole(int id);
    }
}