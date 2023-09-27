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
        private IStationLoopRespository _stationLoopRespository { get; set; }
        private IStationEquipmentRespository _stationEquipmentRespository { get; set; }
        public AlarmService(IAlarmRespository alarmRespository, IStationLoopRespository stationLoopRespository, IStationEquipmentRespository stationEquipmentRespository)
        {
            _alarmRespository = alarmRespository;
            _stationLoopRespository = stationLoopRespository;
            _stationEquipmentRespository = stationEquipmentRespository;
        }

        public Task<List<RealtimeAlarm>> GetRealtimeAlarm(List<string> alarmAreas, List<string> prioritys)
        {
            return Task.Run(() => _alarmRespository.GetRealtimeAlarm(alarmAreas, prioritys));
        }

        public Task<string> AckRealtimeAlarm(List<string> tagNames)
        {
            return Task.Run(() => _alarmRespository.AckRealtimeAlarm(tagNames));
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

        public Task<List<AlarmCount>> GetAlarmCountByStation(Station station)
        {
            return Task.Run(() =>
            {
                var alarms = new List<AlarmCount>();
                var loops = _stationLoopRespository.GetStationLoopsByStation(station.ID);
                var equipments = _stationEquipmentRespository.GetStationEquipmentsBySttaion(station.ID);
                foreach (StationLoop loop in loops)
                {
                    alarms.Add(_alarmRespository.GetAlarmCountByStation(loop.AbbrName, "PT", station.AbbrName, loop.AbbrName));
                    alarms.Add(_alarmRespository.GetAlarmCountByStation(loop.AbbrName, "TT", station.AbbrName, loop.AbbrName));
                    alarms.Add(_alarmRespository.GetAlarmCountByStation(loop.AbbrName, "FM", station.AbbrName, loop.AbbrName));
                    alarms.Add(_alarmRespository.GetAlarmCountByStation(loop.AbbrName, "FC", station.AbbrName, loop.AbbrName));
                }

                foreach (StationEquipment equipment in equipments)
                {
                    alarms.Add(_alarmRespository.GetAlarmCountByStation(equipment.AbbrName, "GC", station.AbbrName, equipment.AbbrName));
                }
                return alarms;
            });

        }
    }
}
