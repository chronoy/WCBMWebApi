using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface IAlarmRespository
    {
        public List<RealtimeAlarm> GetRealtimeAlarm(List<string> alarmAreas, List<string> prioritys);
        public string AckRealtimeAlarm(List<string> tagNames);
        public List<HistoricalAlarm> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys);
        public List<HistoricalStatisticalAlarm> GetHistoricalStatisticalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys);
        public AlarmCount GetAlarmCountByStation(string name, string alarmName, string stationName, string loopName);
        public List<RealtimeAlarm> GetRealtimeAlarmByArea(string alarmAreas, List<string> prioritys);

    }
}
