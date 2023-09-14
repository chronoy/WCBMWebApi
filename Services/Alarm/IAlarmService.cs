﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAlarmService
    {
        public Task<List<RealtimeAlarm>> GetRealtimeAlarm(string alarmArea, string priority);
        public Task<List<DiagnosticAlarm>> GetRealtimeDiagnosticAlarm(int stationID, int loopID);
        public Task<List<HistoricalAlarm>> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, string alarmArea);
        public Task<List<AlarmKPI>> GetHistoricalAlarmKPI(int topNumber, string sortType, DateTime startDateTime, DateTime endDateTime, string alarmArea);
    }
}