using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAlarmService
    {
        public Task<List<RealtimeAlarm>> GetRealtimeAlarm(List<string> alarmAreas, List<string> prioritys);
        public Task<string> AckRealtimeAlarm(List<string> tagNames);
        public Task<List<HistoricalAlarm>> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys);
        public Task<List<HistoricalStatisticalAlarm>> GetHistoricalStatisticalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys);

        public Task<List<AlarmCount>> GetAlarmCountByStation(Station station);

        public Task<List<RealtimeAlarm>> GetRealtimeAlarmByArea(string alarmAreas, List<string> prioritys);
    }
}
