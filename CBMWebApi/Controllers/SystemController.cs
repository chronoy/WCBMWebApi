using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        public async Task<Dictionary<string, object>> SecondaryInterface([FromForm] List<int> companyIDs, [FromForm] List<int> lineIDs)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            try
            {
                List<Station> stations = await _stationService.GetStationsByCompanyLine(companyIDs, lineIDs);
                foreach (Station station in stations)
                {
                    station.StationStatistics["LoopNumber"] = station.Loops.Count;
                    station.StationStatistics["GCNumber"] = station.Equipments.Count;
                    var loopIDs = station.Loops.Select(loop => loop.ID);
                    List<StationLoopDiagnosticData> loopDiagnosticDatas = await _diagnosisService.GetLoopDiagnosticDataByStation(station.ID);
                    List<string> LoopTemperatureInuseTagsNames = station.Loops.Select(loop => station.AbbrName + "_" + loop.AbbrName + "_TemperatureInuse").ToList();
                    List<string> GCC1TagsNames = station.Equipments.Select(quuipment => station.AbbrName + "_" + quuipment.AbbrName + "_C1").ToList();
                    List<PDBTag> stationTags = await _PDBService.GetLoopTagsByStation(station);

                    var CommunicateBadLoop = stationTags.Where(tag => LoopTemperatureInuseTagsNames.Contains(tag.Name) && tag.Value.Contains('?')).ToList();
                    var CommunicateBadGC = stationTags.Where(tag => GCC1TagsNames.Contains(tag.Name) && tag.Value.Contains('?')).ToList();
                    var CommunicateBadLoopNumber = CommunicateBadLoop.Count;
                    var CommunicateBadGCNumber = CommunicateBadGC.Count;
                    station.StationStatistics["CommunicateBadLoopNumber"] = CommunicateBadLoopNumber;
                    station.StationStatistics["CommunicateBadGCNumber"] = CommunicateBadGCNumber;
                    station.StationStatistics["CommunicateGoodLoopNumber"] = station.Loops.Count - CommunicateBadLoopNumber;
                    station.StationStatistics["CommunicateGoodGCNumber"] = station.Equipments.Count - CommunicateBadGCNumber;
                    if ((station.Loops.Count + station.Equipments.Count) != 0)
                        station.StationStatistics["CommunicationGoodRate"] = (((station.Loops.Count - CommunicateBadLoopNumber) + (station.Equipments.Count - CommunicateBadGCNumber)) * 100) / (station.Loops.Count + station.Equipments.Count);
                    else
                    {
                        station.StationStatistics["CommunicationGoodRate"] = 0;
                    }
                    station.StationStatistics["InUseLoopNumber"] = loopDiagnosticDatas.Where(data => loopIDs.Contains(data.ID) && data.LoopStatus.Contains("在用")).Count();
                    List<string> LoopGrossFlowrateTagsNames = station.Loops.Select(loop => station.AbbrName + "_" + loop.AbbrName + "_GrossFlowrate").ToList();
                    List<string> LoopForwordPreDayGrossCumulativeTagsNames = station.Loops.Select(loop => station.AbbrName + "_" + loop.AbbrName + "_ForwordPreDayGrossCumulative").ToList();
                    station.StationStatistics["TotalLoopGrossFlowrate"] = (stationTags.Where(tag => LoopGrossFlowrateTagsNames.Contains(tag.Name) && !tag.Value.Contains("?")).ToList().Select(data => data.Value)).ToList().ConvertAll(s => Convert.ToDouble(s)).Sum();
                    station.StationStatistics["TotalLoopForwordPreDayGrossCumulative"] = (stationTags.Where(tag => LoopForwordPreDayGrossCumulativeTagsNames.Contains(tag.Name) && !tag.Value.Contains("?")).ToList().Select(data => data.Value)).ToList().ConvertAll(s => Convert.ToDouble(s)).Sum();
                    //实时报警
                    List<string> alarmAreas = new List<string>();
                    foreach (StationLoop loop in station.Loops)
                    {
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_FM");
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_FC");
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_PT");
                        alarmAreas.Add(station.AbbrName + "Station_" + loop.AbbrName + "_TT");
                    }
                    station.StationStatistics["HIGHRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(alarmAreas, new List<string>() { "HIGH" })).Count();
                    station.StationStatistics["LOWRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(alarmAreas, new List<string>() { "LOW" })).Count();
                    station.StationStatistics["CRITICALRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(alarmAreas, new List<string>() { "CRITICAL" })).Count();

                    foreach (var loop in station.Loops)
                    {
                        string tagHeader = $"{station.AbbrName}_{loop.AbbrName}";
                        loop.LoopStatistics["CommunicateStatus"] = Convert.ToInt32(!CommunicateBadLoop.Where(x => x.Name == $"{tagHeader}_TemperatureInuse").Any());
                        loop.LoopStatistics["GrossFlowrate"] = stationTags.Where(tag => tag.Name == $"{tagHeader}_GrossFlowrate" && !tag.Value.Contains('?')).ToList().Select(data => Convert.ToDouble(data.Value)).Sum();
                        loop.LoopStatistics["ForwordPreDayGrossCumulative"] = stationTags.Where(tag => tag.Name == $"{tagHeader}_ForwordPreDayGrossCumulative" && !tag.Value.Contains('?')).ToList().Select(data => Convert.ToDouble(data.Value)).Sum();

                        tagHeader = $"{station.AbbrName}Station_{loop.AbbrName}";
                        List<string> loopAlarmAreas = alarmAreas.Where(x => x.Contains(tagHeader)).ToList();
                        loop.LoopStatistics["HIGHRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(loopAlarmAreas, new List<string>() { "HIGH" })).Count;
                        loop.LoopStatistics["LOWRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(loopAlarmAreas, new List<string>() { "LOW" })).Count;
                        loop.LoopStatistics["CRITICALRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(loopAlarmAreas, new List<string>() { "CRITICAL" })).Count;
                    }
                    foreach (var equipment in station.Equipments)
                    {
                        equipment.EquipmentStatistics["CommunicateStatus"] = Convert.ToInt32(!CommunicateBadGC.Where(x => x.Name == $"{station.AbbrName}_{equipment.AbbrName}_C1").Any());
                        
                        List<string> equipmentAlarmAreas = new() { $"{station.AbbrName}Station_{equipment.AbbrName}_GC" };
                        equipment.EquipmentStatistics["HIGHRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(equipmentAlarmAreas, new List<string>() { "HIGH" })).Count;
                        equipment.EquipmentStatistics["LOWRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(equipmentAlarmAreas, new List<string>() { "LOW" })).Count;
                        equipment.EquipmentStatistics["CRITICALRealtimeAlarm"] = (await _alarmService.GetRealtimeAlarm(equipmentAlarmAreas, new List<string>() { "CRITICAL" })).Count;
                    }
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
