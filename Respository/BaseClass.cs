using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Models;

namespace Respository
{
    public class BaseClass
    {
        private readonly SQLServerDBContext _context;
        public BaseClass(SQLServerDBContext context)
        {
            _context = context;
        }
        public List<T> GetEntitys<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return _context.Set<T>().Where(whereLambda).AsNoTracking().ToList();
        }

        public string AddEntity<T>(T entity) where T : class
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                _context.Entry(entity);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string AddEntitys<T>(List<T> entities) where T : class
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Set<T>().AddRange(entities);
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string UpdateEntity<T>(T entity) where T : class
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Set<T>().Update(entity);
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

        public string UpdateEntitys<T>(List<T> entities) where T : class
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Set<T>().UpdateRange(entities);
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public bool DeleteEntity<T>(T entity) where T : class
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Set<T>().Remove(entity);
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

        public bool DeleteEntityBy<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<T> listDeleting = _context.Set<T>().Where(whereLambda).ToList();
                listDeleting.ForEach(u =>
                {
                    _context.Set<T>().Attach(u);
                    _context.Set<T>().Remove(u);
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
