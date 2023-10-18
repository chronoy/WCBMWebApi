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
                List<Station> stations = await _stationService.GetStationsByStations(stationIDs);
                List<StationLoopDiagnosticData> loopStatus = await _diagnosisService.GetLoopStatusByStations(stationIDs);
                foreach (Station station in stations)
                {
                    List<StationLoop> stationLoops = new List<StationLoop>();
                    List<StationEquipment> stationEquipments = new List<StationEquipment>();
                    List<PDBTag> stationTags = await _PDBService.GetLoopTagsByStation(station);
                    List<StationLoopDiagnosticData> loopDiagnosticDatas = await _diagnosisService.GetLoopDiagnosticDataByStation(station.ID);
                    List<StationEquipmentDiagnosticData> equipmentDiagnosticDatas = await _diagnosisService.GetEquipmentDiagnosticDataByStation(station.ID);
                    List<AlarmCount> alarmCounts = await _alarmService.GetAlarmCountByStation(station);
                    foreach (StationLoop loop in station.Loops)
                    {
                        loop.LoopTags = stationTags.Where(tag => tag.Name.Split('_')[1] == loop.AbbrName).ToList();
                        loop.LoopStatus = loopDiagnosticDatas.FirstOrDefault(diagnosticdata => diagnosticdata.ID == loop.ID) == null ? "停用" : loopDiagnosticDatas.FirstOrDefault(diagnosticdata => diagnosticdata.ID == loop.ID).LoopStatus;
                        loop.AlarmCount = alarmCounts.Where(count => count.Name == loop.AbbrName).Count();
                        PDBTag standardTemperature = stationTags.FirstOrDefault(tag => tag.Name.Contains(station.AbbrName + "_" + loop.AbbrName + "_Temperature"));
                        PDBTag standardPressure = stationTags.FirstOrDefault(tag => tag.Name.Contains(station.AbbrName + "_" + loop.AbbrName + "_Pressure"));
                        if (standardTemperature != null && standardPressure != null)
                        {
                            if (standardTemperature.Value.Contains("??") && standardPressure.Value.Contains("??"))
                            {
                                standardTemperature.Value = "25"; standardPressure.Value = "101";
                                bool temParameterStatus = Convert.ToDouble(standardTemperature.Value) == 25 ? true : false;
                                bool preParameterStatus = Convert.ToDouble(standardPressure.Value) == 101 ? true : false;
                                loop.StandardParameterStatus = temParameterStatus && preParameterStatus;
                            }
                        }
                        stationLoops.Add(loop);
                    }
                  
                    foreach (StationEquipment stationEquipment in station.Equipments) 
                    {
                        stationEquipment.EquipmentTags = stationTags.Where(tag => tag.Name.Split('_')[1] == stationEquipment.AbbrName).ToList();
                        stationEquipment.AlarmCount = alarmCounts.Where(x => x.Name == stationEquipment.AbbrName).Count();
                        stationEquipment.EquipmentStatus = equipmentDiagnosticDatas.FirstOrDefault(diagnosticdata => diagnosticdata.ID == stationEquipment.ID) == null ? "停用" : equipmentDiagnosticDatas.FirstOrDefault(diagnosticdata => diagnosticdata.ID == stationEquipment.ID).Result;
                        stationEquipments.Add(stationEquipment);
                    }
                    //station.Loops = stationLoops;
                    //station.Equipments = stationEquipments;
                }
                rtn["Data"] = stations;
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
