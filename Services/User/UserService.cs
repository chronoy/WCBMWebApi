using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Models;
using Respository;

namespace Services
{
    public class UserService : IUserService
    {
        public readonly IUserRespository _userRespository;
        public UserService(IUserRespository userRespository)
        {
            _userRespository = userRespository;

        }
        public string Login(ref User user, string username, string password)
        {
            user = new User();
            switch (_userRespository.GetUserbyLogin(ref user, username, password))
            {
                case "DatabaseError":
                    {
                        return "DatabaseError";
                    }
                case "LogFailed":
                    {
                        return "LogFailed";
                    }
                case "NoSuchUser":
                    {
                        return "LogFailed";
                    }
                default:
                    {
                        switch (_userRespository.GetDepartmentsByUser(ref user))
                        {
                            case "DatabaseError":
                                {
                                    return "DatabaseError";
                                }
                            default:
                                {
                                    return "OK";
                                }
                        }
                    }

            }
        }

        public Task AddUserLogRecord(UserLogRecord userLogRecord)
        {
            return Task.Run(() => _userRespository.AddUserLogRecord(userLogRecord));
        }

        public Task<List<UserLogRecord>> GetUserLogRecords(int userID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _userRespository.GetUserLogRecords(userID, startDateTime, endDateTime));
        }

        public Task<List<User>> GetUsers(Expression<Func<User, bool>> whereLambda)
        {
            return Task.Run(() => _userRespository.GetUsers(whereLambda));
        }

        public Task<List<UserStation>> GetUserStations(Expression<Func<UserStation, bool>> whereLambda)
        {
            return Task.Run(() => _userRespository.GetUserStations(whereLambda));
        }

        public Task<User> AddUser(User user)
        {
            return Task.Run(() =>
            {
                User model = new();
                var userList = _userRespository.GetUsers(x => x.Name == user.Name);
                if (userList.Count > 0)
                {
                    model = userList?.FirstOrDefault() ?? new();
                }
                else
                {
                    _userRespository.AddUser(user);
                    model = user;
                }

                return model;
            });
        }

        public Task<string> UpdateUser(User user)
        {
            return Task.Run(() => _userRespository.UpdateUser(user));
        }

        public Task<bool> DeleteUser(int id)
        {
            return Task.Run(() => _userRespository.DeleteUser(id));
        }

        public Task<string> AddUserStation(List<UserStation> userStations)
        {
            return Task.Run(() => _userRespository.AddUserStation(userStations));
        }

        public Task<bool> DeleteUserStationBy(Expression<Func<UserStation, bool>> whereLambda)
        {
            return Task.Run(() => _userRespository.DeleteUserStationBy(whereLambda));
        }
    }
}
