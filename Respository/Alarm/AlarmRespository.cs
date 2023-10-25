using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Respository
{
    public class AlarmRespository : IAlarmRespository
    {
        private readonly SQLServerDBContext _context;
        public AlarmRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<RealtimeAlarm> GetRealtimeAlarm(List<string> alarmAreas, List<string> prioritys)
        {
            List<RealtimeAlarm> alarms = (from real in _context.RealtimeAlarms
                                          select new RealtimeAlarm
                                          {
                                              ID = real.ID,
                                              StartTime = real.StartTime,
                                              EndTime = real.EndTime,
                                              NodeName = real.NodeName.TrimEnd(),
                                              TagName = real.TagName.TrimEnd(),
                                              Value = real.Value.TrimEnd(),
                                              MessageType = real.MessageType.TrimEnd(),
                                              Description = real.Description.TrimEnd(),
                                              Priority = real.Priority.TrimEnd(),
                                              Status = real.Status.TrimEnd(),
                                              Area = String.Join("_", real.Area.Split(',', StringSplitOptions.None).ToList().GetRange(4, 3)),
                                              OperatorName = real.OperatorName.TrimEnd(),
                                              FullOperatorName = real.FullOperatorName.TrimEnd(),
                                              ACKED = real.ACKED
                                          }).ToList();


            return (from real in alarms
                    where alarmAreas.Contains(real.Area) && prioritys.Contains(real.Priority)
                    select real).OrderByDescending(o => o.StartTime).ToList();
        }

        public string AckRealtimeAlarm(List<string> tagNames)
        {
            using (var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {

                    List<RealtimeAlarm> deletes = (from alarm in _context.RealtimeAlarms
                                                   where alarm.Status == "OK" && tagNames.Contains(alarm.TagName)
                                                   select alarm).ToList();
                    List<RealtimeAlarm> updates = (from alarm in _context.RealtimeAlarms
                                                   where alarm.Status != "OK" && tagNames.Contains(alarm.TagName)
                                                   select new RealtimeAlarm
                                                   {
                                                       ID = alarm.ID,
                                                       StartTime = alarm.StartTime,
                                                       EndTime = alarm.EndTime,
                                                       NodeName = alarm.NodeName,
                                                       TagName = alarm.TagName,
                                                       Value = alarm.Value,
                                                       MessageType = alarm.MessageType,
                                                       Description = alarm.Description,
                                                       Priority = alarm.Priority,
                                                       Status = alarm.Status,
                                                       Area = alarm.Area,
                                                       OperatorName = alarm.OperatorName,
                                                       FullOperatorName = alarm.FullOperatorName,
                                                       ACKED = "ACK"
                                                   }).ToList();

                    _context.RealtimeAlarms.RemoveRange(deletes);
                    _context.RealtimeAlarms.UpdateRange(updates);
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
        }

        public List<RealtimeAlarm> GetRealtimeAlarmByArea(string alarmAreas, List<string> prioritys)
        {
            List<RealtimeAlarm> alarms = (from real in _context.RealtimeAlarms
                                          select new RealtimeAlarm
                                          {
                                              ID = real.ID,
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
                                              ACKED = real.ACKED
                                          }).ToList();


            return (from real in alarms
                    where real.Area.Contains(alarmAreas) && prioritys.Contains(real.Priority)
                    select real).OrderByDescending(o => o.StartTime).ToList();
        }

        public List<HistoricalAlarm> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys)
        {

            List<HistoricalAlarm> alarms = (from alm in _context.HistoricalAlarms
                                            where alm.StartTime >= startDateTime &&
                                                  alm.EndTime <= endDateTime &&
                                                  prioritys.Contains(alm.Priority) &&
                                                  alm.MessageType == "ALARM"
                                            select new HistoricalAlarm
                                            {
                                                HisID = alm.HisID,
                                                StartTime = alm.StartTime,
                                                EndTime = alm.EndTime,
                                                NodeName = alm.NodeName.TrimEnd(),
                                                TagName = alm.TagName.TrimEnd(),
                                                Value = alm.Value.TrimEnd(),
                                                MessageType = alm.MessageType.TrimEnd(),
                                                Description = alm.Description.TrimEnd(),
                                                Priority = alm.Priority.TrimEnd(),
                                                Status = alm.Status.TrimEnd(),
                                                Area = String.Join("_", alm.Area.Split(',', StringSplitOptions.None).ToList().GetRange(4, 3)),
                                                OperatorName = alm.OperatorName.TrimEnd(),
                                                FullOperatorName = alm.FullOperatorName.TrimEnd(),
                                            }).ToList();

            var historicalAlarms = (from alm in alarms
                                    where alarmAreas.Contains(alm.Area)
                                    select new HistoricalAlarm
                                    {
                                        HisID = alm.HisID,
                                        StartTime = alm.StartTime,
                                        EndTime = alm.EndTime,
                                        NodeName = alm.NodeName.TrimEnd(),
                                        TagName = alm.TagName.TrimEnd(),
                                        Value = alm.Value.TrimEnd(),
                                        MessageType = alm.MessageType.TrimEnd(),
                                        Description = alm.Description.TrimEnd(),
                                        Priority = alm.Priority.TrimEnd(),
                                        Status = alm.Status.TrimEnd(),
                                        Area = alm.Area.TrimEnd(),
                                        OperatorName = alm.OperatorName.TrimEnd(),
                                        FullOperatorName = alm.FullOperatorName.TrimEnd(),
                                    }).OrderByDescending(o => o.StartTime).ThenByDescending(t => t.EndTime).ToList();
            return historicalAlarms;
        }
        
        public List<HistoricalStatisticalAlarm> GetHistoricalStatisticalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys)
        {

            List<HistoricalAlarm> alarms = (from alm in _context.HistoricalAlarms
                                            where alm.StartTime >= startDateTime &&
                                                  alm.EndTime <= endDateTime &&
                                                  prioritys.Contains(alm.Priority) &&
                                                  alm.MessageType == "ALARM" &&
                                                  alm.Status == "OK"
                                            select new HistoricalAlarm
                                            {
                                                HisID = alm.HisID,
                                                StartTime = alm.StartTime,
                                                EndTime = alm.EndTime,
                                                NodeName = alm.NodeName.TrimEnd(),
                                                TagName = alm.TagName.TrimEnd(),
                                                Value = alm.Value.TrimEnd(),
                                                MessageType = alm.MessageType.TrimEnd(),
                                                Description = alm.Description.TrimEnd(),
                                                Priority = alm.Priority.TrimEnd(),
                                                Status = alm.Status.TrimEnd(),
                                                Area = String.Join("_", alm.Area.Split(',', StringSplitOptions.None).ToList().GetRange(4, 3)),
                                                OperatorName = alm.OperatorName.TrimEnd(),
                                                FullOperatorName = alm.FullOperatorName.TrimEnd(),
                                            }).ToList();

            List<HistoricalAlarm> historicalAlarms = (from alm in alarms
                                                      where alarmAreas.Contains(alm.Area)
                                                      select alm
                                                      ).ToList();

            List<HistoricalStatisticalAlarm> statisticalAlarms = (from alm in alarms
                                                                  where alarmAreas.Contains(alm.Area)
                                                                  group alm by new
                                                                  {
                                                                      alm.TagName,
                                                                      alm.Description
                                                                  } into almGroup
                                                                  select new HistoricalStatisticalAlarm
                                                                  {
                                                                      TagName = almGroup.Key.TagName,
                                                                      Description = almGroup.Key.Description,
                                                                      StartTime = almGroup.Min(grp => grp.StartTime),
                                                                      EndTime = almGroup.Max(grp => grp.EndTime),
                                                                      Duration = TimeSpan.FromSeconds(almGroup.Sum(grp => (grp.EndTime - grp.StartTime).TotalSeconds)),
                                                                      Count = almGroup.Count()
                                                                  }).ToList();

            return statisticalAlarms;
        } 
        
        public AlarmCount GetAlarmCountByStation(string name, string alarmName, string stationName, string loopName)
        {
            var alarms = (from alarm in _context.RealtimeAlarms
                          where alarm.MessageType == "ALARM" && alarm.Status != "OK" &&
                          alarm.Area.Contains(stationName) && alarm.Area.Contains(loopName) &&
                          alarm.Area.Contains(alarmName)
                          select alarm).ToList();
            return new AlarmCount { Name = name, AlarmName = alarmName, AlarmArea = $"{stationName}_{loopName}_{alarmName}", Count = alarms.Count };
        }
    }
}
