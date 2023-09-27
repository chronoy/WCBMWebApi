using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Models;

namespace Respository
{
    public  interface IUserRespository
    {
        public string GetDepartmentsByUser(ref User user);

        public string GetUserbyLogin(ref User user, string username, string passwerd);

        public void AddUserLogRecord(UserLogRecord userLogRecord);
        public List<UserLogRecord> GetUserLogRecords(int userID, DateTime startDateTime, DateTime endDateTime);

        public List<User> GetUsers(Expression<Func<User, bool>> whereLambda);
        public List<UserStation> GetUserStations(Expression<Func<UserStation, bool>> whereLambda);
        public string AddUser(User user);
        public string UpdateUser(User user);
        public bool DeleteUser(int id);
        public string AddUserStation(List<UserStation> userStations);
        public bool DeleteUserStationBy(Expression<Func<UserStation, bool>> whereLambda);
    }
}
