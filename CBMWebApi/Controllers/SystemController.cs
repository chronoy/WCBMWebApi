using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using static System.Collections.Specialized.BitVector32;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly IAlarmService _alarmService;
        private readonly IDiagnosisService _diagnosisService;
        private readonly IStationService _stationService;
        private readonly IPDBService _PDBService;
        public SystemController(IAlarmService alarmService,
                                 IDiagnosisService diagnosisService,
                                 IStationService stationService,

                                 IPDBService PDBService
                                 )
        {
            _alarmService = alarmService;
            _diagnosisService = diagnosisService;
            _stationService = stationService;
            _PDBService = PDBService;
        }
        [HttpPost]
        public async Task<Dictionary<string, object>> FirstInterface([FromForm] List<int> stationIDs, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                //回路状态
                List<Station> stations = await _stationService.GetStationsByStations(stationIDs);
                List<StationLoopDiagnosticData> loopStatus = await _diagnosisService.GetLoopStatusByStations(stationIDs);
                data["loopStatusDetail"] = loopStatus;
                Dictionary<string, object> loopStatusCount = new Dictionary<string, object>();
                loopStatusCount["InUseLoopCount"] = loopStatus.Where(loopStatus => loopStatus.LoopStatus.Equals("在用")).Count();
                loopStatusCount["NotInUseLoopCount"] = loopStatus.Where(loopStatus => !loopStatus.LoopStatus.Equals("在用")).Count();
                data["loopStatusStatistics"] = loopStatusCount;

                //实时报警
                List<string> alarmAreas = new List<string>();
                foreach (Station station in stations)
                {
                    foreach (StationLoop loop in station.Loops)
                    {
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_FM");
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_FC");
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_PT");
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_TT");
                    }
                }
                var realtimeAlarm = await _alarmService.GetRealtimeAlarm(alarmAreas, new List<string>() { "HIGH", "LOW", "CRITICAL" });
                Dictionary<string, object> alarmCount = new Dictionary<string, object>();
                alarmCount["CRITICAL"] = realtimeAlarm.Where(alarm => alarm.Priority.Contains("CRITICAL")).Count();
                alarmCount["HIGH"] = realtimeAlarm.Where(alarm => alarm.Priority.Contains("HIGH")).Count();
                alarmCount["LOW"] = realtimeAlarm.Where(alarm => alarm.Priority.Contains("LOW")).Count();
                data["realtimeAlarmStatistics"] = alarmCount;
                //历史报警
                var hisAlarm = await _alarmService.GetHistoricalStatisticalAlarm(startDateTime, endDateTime, alarmAreas, new List<string>() { "HIGH", "LOW", "CRITICAL" });
                data["historicalAlarmStatistics"] = hisAlarm.OrderBy(obj => obj.Duration).Take(10).ToList();
                rtn["Data"] = data;
                rtn["MSG"] = "OK";
                rtn["Code"] = 200;
            }
            catch (Exception ex)
            {

                rtn["Data"] = new Dictionary<string, object>();
                rtn["MSG"] = ex.Message;
                rtn["Code"] = 400;
            }
          
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> SecondaryInterface([FromForm] List<int> stationIDs)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            try
            {
                List<Station> stationsData = new List<Station>();
                List<Station> stations = await _stationService.GetStationsByStations(stationIDs);
                List<StationLoopDiagnosticData> loopStatus = await _diagnosisService.GetLoopStatusByStations(stationIDs);
                foreach (Station station in stations)
                {
                    //回路状态
                    List<PDBTag> stationTags = await _PDBService.GetLoopTagsByStation(station);
                    var stationLoopStatus = loopStatus.Where(looopStatus => looopStatus.StationName.Equals(station.Name)).ToList();
                    station.StationStatistics["loopStatusDetail"] = stationLoopStatus;
                    Dictionary<string, object> loopStatusCount = new Dictionary<string, object>();
                    loopStatusCount["InUseLoopCount"] = loopStatus.Where(loopStatus => loopStatus.LoopStatus.Equals("在用")).Count();
                    loopStatusCount["NotInUseLoopCount"] = loopStatus.Where(loopStatus => !loopStatus.LoopStatus.Equals("在用")).Count();
                    station.StationStatistics["loopStatusStatistics"] = loopStatusCount;
                    //参比条件
                    List<Dictionary<string, object>> standardloopsParameter = new List<Dictionary<string, object>>();
                    Dictionary<string, object> loopParameterStandard = new Dictionary<string, object>();
                    Dictionary<string, object> standardloopsParameterStatistics = new Dictionary<string, object>();
                    foreach (StationLoop loop in station.Loops)
                    {  
                        PDBTag standardTemperature = stationTags.FirstOrDefault(tag => tag.Name.Contains(station.AbbrName + "_" + loop.AbbrName + "_Temperature"));
                        PDBTag standardPressure = stationTags.FirstOrDefault(tag => tag.Name.Contains(station.AbbrName + "_" + loop.AbbrName + "_Pressure"));
                        if (standardTemperature != null && standardPressure != null)
                        {
                            
                            if (standardTemperature.Value.Contains("??") && standardPressure.Value.Contains("??"))
                            {
                                standardTemperature.Value = "25"; standardPressure.Value = "101";
                                loopParameterStandard["LoopAbbrName"] = loop.AbbrName;
                                loopParameterStandard["StationAbbrName"] = station.AbbrName;
                                loopParameterStandard["StandardTemperature"] = Convert.ToDouble(standardTemperature.Value) == 25 ? true : false;
                                loopParameterStandard["StandardPressure"] = Convert.ToDouble(standardPressure.Value) == 101 ? true : false;
                                standardloopsParameter.Add(loopParameterStandard);
                            }
                        }
                    }
                    station.StationStatistics["StandardloopsParameterDetail"] = standardloopsParameter;

                    //报警
                    var realtimeAlarm = await _alarmService.GetRealtimeAlarmByArea(station.AbbrName + "Station", new List<string>() { "HIGH", "LOW", "CRITICAL" });
                    Dictionary<string, object> alarmCount = new Dictionary<string, object>();
                    alarmCount["CRITICAL"] = realtimeAlarm.Where(alarm => alarm.Priority.Contains("CRITICAL")).Count();
                    alarmCount["HIGH"] = realtimeAlarm.Where(alarm => alarm.Priority.Contains("HIGH")).Count();
                    alarmCount["LOW"] = realtimeAlarm.Where(alarm => alarm.Priority.Contains("LOW")).Count();
                    station.StationStatistics["realtimeAlarmStatistics"] = alarmCount;
                    station.StationStatistics["AlarmDetail"] = realtimeAlarm;
                    station.Loops = new List<StationLoop>();
                    stationsData.Add(station);
                }
                rtn["Data"] = stationsData;
                rtn["MSG"] = "OK";
                rtn["Code"] = 200;
            }
            catch (Exception ex)
            {

                rtn["Data"] = new Dictionary<string, object>();
                rtn["MSG"] = ex.Message;
                rtn["Code"] = 400;
            }
           
            return rtn;
        }
    }
}
