using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Numerics;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;

namespace Respository
{
    public class UserRespository : IUserRespository
    {
        private readonly SQLServerDBContext _context;
        public UserRespository(SQLServerDBContext context)
        {
            _context = context;
        }


        public string GetDepartmentsByUser(ref User user)
        {
            try
            {
                int userID = user.ID;
                var userStation = _context.UserStations.Where(x => x.UserID == userID).ToList();
                user.Stations = (from u in userStation
                                 join s in _context.Stations on u.StationID equals s.ID
                                 join c in _context.Collectors on s.CollectorID equals c.ID into tempc
                                 from tc in tempc.DefaultIfEmpty()
                                 select new Station
                                 {
                                     ID = s.ID,
                                     Name = s.Name,
                                     AbbrName = s.AbbrName,
                                     CollectorID = s.CollectorID,
                                     AreaID = s.AreaID,
                                     IPAddress = tc != null ? tc.IPAddress : "",
                                     IPPort = tc != null ? tc.IPPort : ""
                                 }).ToList();
                user.Loops = (from u in userStation
                              join l in _context.StationLoops on u.StationID equals l.StationID
                              select l).ToList();
                user.Equipments = (from u in userStation
                                   join e in _context.StationEquipments on u.StationID equals e.StationID
                                   join category in _context.EquipmentCategories on e.EquipmentCategoryID equals category.ID into tempc
                                   from tc in tempc.DefaultIfEmpty()
                                   select new StationEquipment
                                   {
                                       ID = e.ID,
                                       Name = e.Name,
                                       AbbrName = e.AbbrName,
                                       StationID = e.StationID,
                                       CollectDataTypeID = e.CollectDataTypeID,
                                       EquipmentCategoryID = e.EquipmentCategoryID,
                                       //EquipmentCategoryName = tc != null ? tc.Name : "",
                                       Manufacturer = e.Manufacturer,
                                       Model = e.Model
                                   }).ToList();
                user.Companies = (from u in userStation
                                  join s in _context.Stations on u.StationID equals s.ID
                                  join a in _context.Areas on s.AreaID equals a.ID
                                  join c in _context.companies on a.CompanyID equals c.ID
                                  select new Company
                                  {
                                      ID = u.StationID,
                                      Name = c.Name,
                                      AbbrName = c.AbbrName,
                                      FullName = c.FullName
                                  }).ToList();
                user.Areas = (from u in userStation
                              join s in _context.Stations on u.StationID equals s.ID
                              join a in _context.Areas on s.AreaID equals a.ID
                              select new Area
                              {
                                  ID = a.ID,
                                  Name = a.Name,
                                  FullName = a.FullName,
                                  AbbrName = a.AbbrName,
                                  CompanyID = a.CompanyID
                              }).ToList();
                return "OK";
            }
            catch (Exception ex)
            {
                return "DatabaseError";
            }
        }

        public string GetUserbyLogin(ref User user, string username, string password)
        {
            try
            {
                List<User> userList = (from u in _context.Users
                                       join r in _context.Roles on u.RoleID equals r.ID into temp
                                       from t in temp.DefaultIfEmpty()
                                       where u.Name == username
                                       select new User
                                       {
                                           ID = u.ID,
                                           Name = u.Name,
                                           Password = u.Password,
                                           PersonName = u.PersonName,
                                           ContactNumber = u.ContactNumber,
                                           RoleID = u.RoleID,
                                           RoleName = t != null ? t.Name : ""
                                       }).ToList();

                if (userList.Count > 0)
                {
                    user = userList.First();

                    if (Base64UrlEncoder.Encode(SHA256.HashData(Encoding.ASCII.GetBytes(user.Password))) == password)
                    {
                        return "OK";
                    }
                    else
                    {
                        return "LogFailed";
                    }
                }
                else
                {
                    return "NoSuchUser";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "DatabaseError";
            }
        }

        public void AddUserLogRecord(UserLogRecord userLogRecord)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.UserLogRecords.Add(userLogRecord);
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
            }

        }

        public List<UserLogRecord> GetUserLogRecords(int userID, DateTime startDateTime, DateTime endDateTime)
        {
            List<UserLogRecord> userLogRecord = new();

            userLogRecord = _context.UserLogRecords.Where(x => x.DateTime >= startDateTime && x.DateTime <= endDateTime).OrderByDescending(x => x.DateTime).ToList();

            return userLogRecord;
        }

        public List<User> GetUsers(Expression<Func<User, bool>> whereLambda)
        {
            var data = _context.Users.Where(whereLambda);

            List<User> userList = (from u in data
                                   join r in _context.Roles on u.RoleID equals r.ID into temp
                                   from t in temp.DefaultIfEmpty()
                                   select new User
                                   {
                                       ID = u.ID,
                                       Name = u.Name,
                                       Password = u.Password,
                                       PersonName = u.PersonName,
                                       ContactNumber = u.ContactNumber,
                                       RoleID = u.RoleID,
                                       RoleName = t != null ? t.Name : ""
                                   }).ToList();
            return userList;
        }

        public List<UserStation> GetUserStations(Expression<Func<UserStation, bool>> whereLambda)
        {
            return _context.UserStations.Where(whereLambda).ToList();
        }

        public string AddUser(User user)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                _context.Entry(user);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string UpdateUser(User user)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.Users.Update(user);
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

        public bool DeleteUser(int id)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<User> listDeleting = _context.Users.Where(x => x.ID == id).ToList();
                listDeleting.ForEach(u =>
                {
                    _context.Users.Attach(u);
                    _context.Users.Remove(u);
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

        public string AddUserStation(List<UserStation> userStations)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.UserStations.AddRange(userStations);
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

        public bool DeleteUserStationBy(Expression<Func<UserStation, bool>> whereLambda)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<UserStation> listDeleting = _context.UserStations.Where(whereLambda).ToList();
                listDeleting.ForEach(u =>
                {
                    _context.UserStations.Attach(u);
                    _context.UserStations.Remove(u);
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
