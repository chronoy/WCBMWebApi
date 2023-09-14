using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Numerics;

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
                user.companies = (from u in _context.UserStations
                                  join c in _context.companies on u.StationID equals c.ID
                                  where u.UserID == userID
                                  select new Company
                                  {
                                      ID = u.StationID,
                                      Name = c.Name,
                                      AbbrName = c.AbbrName,
                                      FullName = c.FullName,
                                      Areas = (from a in _context.Areas
                                               where a.CompanyID == c.ID
                                               select new Area
                                               {
                                                   ID = a.ID,
                                                   Name = a.Name,
                                                   FullName = a.FullName,
                                                   AbbrName = a.AbbrName,
                                                   CompanyID = a.CompanyID,
                                                   Stations = (from s in _context.Stations
                                                               where s.AreaID == a.ID
                                                               join c in _context.Collectors on s.CollectorID equals c.ID into tempc
                                                               from tc in tempc.DefaultIfEmpty()
                                                               select new Station
                                                               {
                                                                   ID = s.ID,
                                                                   Name = s.Name,
                                                                   AbbrName = s.AbbrName,
                                                                   CollectorID = s.CollectorID,
                                                                   AreaID = s.AreaID,
                                                                   IPAddress = tc.IPAddress,
                                                                   IPPort = tc.IPPort,
                                                                   Loops = (from l in _context.StationLoops
                                                                            where l.StationID == s.ID
                                                                            select l).ToList(),
                                                                   Equipments = (from e in _context.StationEquipments
                                                                                 where e.StationID == s.ID
                                                                                 join type in _context.EquipmentCategories on e.EquipmentCategoryID equals type.ID
                                                                                 select new StationEquipment
                                                                                 {
                                                                                     ID = e.ID,
                                                                                     Name = e.Name,
                                                                                     AbbrName = e.AbbrName,
                                                                                     EquipmentCategoryID = e.EquipmentCategoryID,
                                                                                     EquipmentCategoryName = type.Name,
                                                                                     CollectDataTypeID = e.CollectDataTypeID,
                                                                                     Manufacturer = e.Manufacturer,
                                                                                     Model = e.Model,
                                                                                     StationID = e.StationID
                                                                                 }).ToList()
                                                               }).ToList()
                                               }).ToList()
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

                    if (EncryptString(user.Password) == password)
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

        public static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }
    }
}
