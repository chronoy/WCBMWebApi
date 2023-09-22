using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public  interface IUserService
    {
        public string Login(ref User user,string username, string password);

        public Task AddUserLogRecord(UserLogRecord userLogRecord);

        public Task<List<UserLogRecord>> GetUserLogRecords(int userID, DateTime startDateTime, DateTime endDateTime);

        public Task<List<User>> GetUsers(Expression<Func<User, bool>> whereLambda);
        public Task<List<UserStation>> GetUserStations(Expression<Func<UserStation, bool>> whereLambda);
        public Task<User> AddUser(User user);
        public Task<string> UpdateUser(User user);
        public Task<bool> DeleteUser(int id);
        public Task<string> AddUserStation(List<UserStation> userStations);
        public Task<bool> DeleteUserStationBy(Expression<Func<UserStation, bool>> whereLambda);
    }
}
