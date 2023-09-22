using Models;

namespace Respository
{
    public interface IRoleRespository
    {
        public List<Role> GetRoles();
        public string AddRole(Role role);
        public string UpdateRole(Role role);
        public bool DeleteRole(int id);
    }
}