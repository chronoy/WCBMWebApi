﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class EquipmentRespository : IEquipmentRespository
    {
        private readonly SQLServerDBContext _context;
        public EquipmentRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<T> GetEquipmentParameters<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return _context.Set<T>().Where(whereLambda).AsNoTracking().ToList();
        }

        public string AddEquipmentParameter<T>(T entity) where T : class
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

        public string AddEquipmentParameters<T>(List<T> entities) where T : class
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

        public bool UpdateEquipmentParameter<T>(T entity) where T : class
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Set<T>().Update(entity);
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

        public bool UpdateEquipmentParameters<T>(List<T> listEntity) where T : class
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Set<T>().UpdateRange(listEntity);
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

        public bool DeleteEquipmentParameter<T>(T entity) where T : class
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

        public bool DeleteEquipmentParameterBy<T>(Expression<Func<T, bool>> whereLambda) where T : class
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
