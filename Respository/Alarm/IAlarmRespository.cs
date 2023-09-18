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
        public List<RealtimeAlarm> GetRealtimeAlarm(string alarmArea, string priority);
        public List<RealtimeDiagnosticAlarm> GetRealtimeDiagnosticAlarm(int stationID, int loopID);
        public List<HistoricalAlarm> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, string alarmArea);
        public List<AlarmKPI> GetHistoricalAlarmKPI(int topNumber, string sortType, DateTime startDateTime, DateTime endDateTime, string alarmArea);
        public AlarmCount GetAlarmCountByStation(string name, string alarmName, string alarmArea);
    }
}
