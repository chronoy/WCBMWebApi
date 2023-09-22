using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class RoleRespository: IRoleRespository
    {
        private readonly SQLServerDBContext _context;
        public RoleRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public string AddRole(Role role)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                _context.Entry(role);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string UpdateRole(Role role)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Roles.Update(role);
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return "OtherError";
            }
            return "OK";
        }

        public bool DeleteRole(int id)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<Role> listDeleting = _context.Roles.Where(x => x.ID == id).ToList();
                listDeleting.ForEach(u =>
                {
                    _context.Roles.Attach(u);
                    _context.Roles.Remove(u);
                });
                result = _context.SaveChanges() > 0;
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return result;
            }
            return result;
        }
    }
}
