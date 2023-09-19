using System;
using System.Collections.Generic;
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
    }
}
