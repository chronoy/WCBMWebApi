﻿using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class AlarmRespository : IAlarmRespository
    {
        private readonly SQLServerDBContext _context;
        public AlarmRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<RealtimeAlarm> GetRealtimeAlarm(string alarmArea, string priority)
        {
            var realtimeAlarm = (from real in _context.RealtimeAlarms
                                 where real.Area.Contains(alarmArea) && real.Status != "OK"
                                 select new RealtimeAlarm
                                 {
                                     StartTime = real.StartTime,
                                     EndTime = real.EndTime,
                                     NodeName = real.NodeName.TrimEnd(),
                                     TagName = real.TagName.TrimEnd(),
                                     Value = real.Value.TrimEnd(),
                                     MessageType = real.MessageType.TrimEnd(),
                                     Description = real.Description.TrimEnd(),
                                     Priority = real.Priority.TrimEnd(),
                                     Status = real.Status.TrimEnd(),
                                     Area = real.Area.TrimEnd(),
                                     OperatorName = real.OperatorName.TrimEnd(),
                                     FullOperatorName = real.FullOperatorName.TrimEnd(),
                                 }).OrderByDescending(o => o.StartTime).ThenByDescending(t => t.EndTime).ToList();

            if (priority != "%")
            {
                realtimeAlarm = realtimeAlarm.Where(x => x.Priority.Contains(priority)).OrderByDescending(o => o.StartTime).ThenByDescending(t => t.EndTime).ToList();
            }

            return realtimeAlarm;
        }

        public List<DiagnosticAlarm> GetRealtimeDiagnosticAlarm(int stationID, int loopID)
        {

            List<DiagnosticAlarm> diagnostics = new();
            var data = (from d in _context.RealtimeDiagnosticAlarms
                        join loop in _context.StationLoops on d.LoopID equals loop.ID
                        where d.Status != "OK"
                        select new
                        {
                            d.StartTime,
                            d.EndTime,
                            Description = $"{loop.Name} {d.Description.Trim()}",
                            d.DiagnosticResult,
                            Value = Math.Round(d.Value, 2),
                            d.Status,
                            loop.StationID,
                            d.LoopID
                        }).ToList();

            if (loopID == -1)
            {
                diagnostics = (from d in data
                               where d.StationID == stationID
                               select new DiagnosticAlarm
                               {
                                   StartTime = d.StartTime,
                                   EndTime = d.EndTime,
                                   Description = d.Description,
                                   DiagnosticResult = d.DiagnosticResult,
                                   Value = d.Value,
                                   Status = d.Status
                               }).ToList();
            }
            else
            {
                diagnostics = (from d in data
                               where d.LoopID == loopID
                               select new DiagnosticAlarm
                               {
                                   StartTime = d.StartTime,
                                   EndTime = d.EndTime,
                                   Description = d.Description,
                                   DiagnosticResult = d.DiagnosticResult,
                                   Value = d.Value,
                                   Status = d.Status
                               }).ToList();
            }

            return diagnostics;
        }

        public List<HistoricalAlarm> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, string alarmArea)
        {
            var historicalAlarm = (from his in _context.HistoricalAlarms
                                   where his.StartTime > startDateTime && his.EndTime < endDateTime &&
                                   his.MessageType.Contains("ALARM") && his.Area.Contains(alarmArea)
                                   select new HistoricalAlarm
                                   {
                                       HisID = his.HisID,
                                       StartTime = his.StartTime,
                                       EndTime = his.EndTime,
                                       NodeName = his.NodeName.TrimEnd(),
                                       TagName = his.TagName.TrimEnd(),
                                       Value = his.Value.TrimEnd(),
                                       MessageType = his.MessageType.TrimEnd(),
                                       Description = his.Description.TrimEnd(),
                                       Priority = his.Priority.TrimEnd(),
                                       Status = his.Status.TrimEnd(),
                                       Area = his.Area.TrimEnd(),
                                       OperatorName = his.OperatorName.TrimEnd(),
                                       FullOperatorName = his.FullOperatorName.TrimEnd(),
                                   }).OrderByDescending(o => o.StartTime).ThenByDescending(t => t.EndTime).ToList();
            return historicalAlarm;
        }

        public List<AlarmKPI> GetHistoricalAlarmKPI(int topNumber, string sortType, DateTime startDateTime, DateTime endDateTime, string alarmArea)
        {
            var historicalAlarmKPI = (from kpi in
                                          (from his in _context.HistoricalAlarms.ToList()
                                           where his.Status.Contains("OK") && his.StartTime >= startDateTime &&
                                           his.EndTime <= endDateTime && his.Area.Contains(alarmArea)
                                           group his by new { his.Description, his.Status } into g
                                           select new
                                           {
                                               Description = g.Key.Description.TrimEnd(),
                                               Duration = g.Sum(s => s.EndTime.Subtract(s.StartTime).TotalSeconds),
                                               Status = g.Key.Status.TrimEnd(),
                                               AlarmCount = g.Count()
                                           }).ToList()
                                      select new AlarmKPI
                                      {
                                          Description = kpi.Description,
                                          Status = kpi.Status,
                                          DurationValue = Convert.ToInt32(kpi.Duration),
                                          Duration = $"{TimeSpan.FromSeconds(kpi.Duration).Days}天{TimeSpan.FromSeconds(kpi.Duration).Hours}时{TimeSpan.FromSeconds(kpi.Duration).Minutes}分{TimeSpan.FromSeconds(kpi.Duration).Seconds}秒",
                                          AlarmCount = kpi.AlarmCount
                                      }).OrderBy(o => GetPropertyValue(o, sortType)).Take(topNumber).ToList();
            return historicalAlarmKPI;
        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }
    }
}