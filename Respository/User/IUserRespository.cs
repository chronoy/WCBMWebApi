﻿using System;
using System.Collections.Generic;
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
    }
}
