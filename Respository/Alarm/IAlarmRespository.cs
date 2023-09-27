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
        public List<DiagnosticAlarm> GetRealtimeDiagnosticAlarm(int stationID, int loopID);
        public List<HistoricalAlarm> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys);
        public List<HistoricalStatisticalAlarm> GetHistoricalStatisticalAlarm(DateTime startDateTime, DateTime endDateTime, List<string> alarmAreas, List<string> prioritys);
        public List<AlarmKPI> GetHistoricalAlarmKPI(int topNumber, string sortType, DateTime startDateTime, DateTime endDateTime, string alarmArea);
        public AlarmCount GetAlarmCountByStation(string name, string alarmName, string alarmArea);
        public List<RealtimeAlarm> GetRealtimeAlarmByArea(string alarmAreas, List<string> prioritys);

    }
}
