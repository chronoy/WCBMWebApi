using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AlarmService : IAlarmService
    {
        private IAlarmRespository _alarmRespository;
        public AlarmService(IAlarmRespository alarmRespository)
        {
            _alarmRespository = alarmRespository;
        }

        public Task<List<RealtimeAlarm>> GetRealtimeAlarm(string alarmArea, string priority)
        {
            return Task.Run(() => _alarmRespository.GetRealtimeAlarm(alarmArea, priority));
        }

        public Task<List<DiagnosticAlarm>> GetRealtimeDiagnosticAlarm(int stationID, int loopID)
        {
            return Task.Run(() => _alarmRespository.GetRealtimeDiagnosticAlarm(stationID, loopID));
        }

        public Task<List<HistoricalAlarm>> GetHistoricalAlarm(DateTime startDateTime, DateTime endDateTime, string alarmArea)
        {
            return Task.Run(() => _alarmRespository.GetHistoricalAlarm(startDateTime, endDateTime, alarmArea));
        }
        public Task<List<AlarmKPI>> GetHistoricalAlarmKPI(int topNumber, string sortType, DateTime startDateTime, DateTime endDateTime, string alarmArea)
        {
            return Task.Run(() => _alarmRespository.GetHistoricalAlarmKPI(topNumber, sortType, startDateTime, endDateTime, alarmArea));
        }
    }
}
