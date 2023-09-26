using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Models;
using Services;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using static System.Collections.Specialized.BitVector32;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IAlarmService _alarmService;
        private readonly IDiagnosisService _diagnosisService;
        private readonly IStationService _stationService;
        private readonly IStationLoopService _stationLoopService;
        private readonly IStationEquipmentService _stationEquipmentService;
        private readonly IPDBService _PDBService;
        public StationController(IAlarmService alarmService,
                                 IDiagnosisService diagnosisService,
                                 IStationService stationService,
                                 IStationLoopService stationLoopService,
                                 IStationEquipmentService stationEquipmentService,
                                 IPDBService PDBService
                                 )
        {
            _alarmService = alarmService;
            _diagnosisService = diagnosisService;
            _stationService = stationService;
            _stationLoopService = stationLoopService;
            _stationEquipmentService = stationEquipmentService;
            _PDBService = PDBService;
        }

        // GET: api/<StationController>
        [HttpPost]
        public async Task<Dictionary<string, object>> GetStationData([FromForm] int stationID)
        {
            Dictionary<string, object> rtn = new();
            Station station = await _stationService.GetStationByID(stationID);
            List<PDBTag> stationTags = await _PDBService.GetLoopTagsByStation(station);
            List<StationLoop> loops = await _stationLoopService.GetStationLoopsByStation(stationID);
            List<StationEquipment> equipments = await _stationEquipmentService.GetStationEquipmentsBySttaion(stationID);
            List<AlarmCount> alarmCounts = await _alarmService.GetAlarmCountByStation(station);
            List<StationLoopDiagnosticData> loopDiagnosticDatas = await _diagnosisService.GetLoopDiagnosticDataByStation(station.ID);
            List<StationEquipmentDiagnosticData> equipmentDiagnosticDatas = await _diagnosisService.GetEquipmentDiagnosticDataByStation(station.ID);
            List<List<PDBTag>> loopTags = new List<List<PDBTag>>();
            List<List<PDBTag>> EquipmentTags = new List<List<PDBTag>>();
            List<List<AlarmCount>> loopAlarmCount = new List<List<AlarmCount>>();
            List<List<AlarmCount>> equipmentAlarmCount = new List<List<AlarmCount>>();
            foreach (StationLoop loop in loops)
            {
                loopTags.Add(stationTags.Where(tag => tag.Name.Split('_')[1] == loop.AbbrName).ToList());
                loopAlarmCount.Add(alarmCounts.Where(x => x.Name == loop.AbbrName).ToList());
            }
            foreach (StationEquipment equipment in equipments)
            {
                EquipmentTags.Add(stationTags.Where(tag => tag.Name.Split('_')[1] == equipment.AbbrName).ToList());
                equipmentAlarmCount.Add(alarmCounts.Where(x => x.Name == equipment.AbbrName).ToList());
            }


            rtn["Loops"] = loops;
            rtn["Equipments"] = equipments;
            rtn["LoopTags"] = loopTags;
            rtn["EquipmentTags"] = EquipmentTags;
            rtn["LoopDiagnostic"] = loopDiagnosticDatas;
            rtn["EquipmentDiagnostic"] = equipmentDiagnosticDatas;
            rtn["LoopAlarmCount"] = loopAlarmCount;
            rtn["EquipmentAlarmCount"] = equipmentAlarmCount;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetLoopDiagnosticDataDetailByLoop([FromForm] int loopID, [FromForm] string manufacturer, [FromForm] string diagnosisType)
        {
            Dictionary<string, object> rtn = new();
            List<DiagnosticDataDetail> details = await _diagnosisService.GetLoopDiagnosticDataDetailByLoop(loopID, manufacturer, diagnosisType);
            if (details == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = details;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentDiagnosticDataDetailByEquipment([FromForm] int equipmentID, [FromForm] string manufacturer)
        {
            Dictionary<string, object> rtn = new();
            List<DiagnosticDataDetail> details = await _diagnosisService.GetEquipmentDiagnosticDataDetailByEquipment(equipmentID, manufacturer);
            if (details == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = details;
            return rtn;
        }
        [HttpPost]
        public async Task<Dictionary<string, object>> GetStationStatistics([FromForm] List<int> stationIDs)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<Station> stationsData = new List<Station>();
            List<Station> stations = await _stationService.GetStationsByStations(stationIDs);
            List<StationLoopDiagnosticData> loopStatus = await _diagnosisService.GetLoopStatusByStations(stationIDs);
            foreach (Station station in stations)
            {
                List<PDBTag> stationTags = await _PDBService.GetLoopTagsByStation(station);
                var stationLoopStatus = loopStatus.Where(looopStatus => looopStatus.StationName.Equals(station.Name)).ToList();
                station.StationStatistics["loopStatus"] = stationLoopStatus;
                List<Dictionary<string, object>> loopParameterStandards = new List<Dictionary<string, object>>();
                Dictionary<string, object> loopParameterStandard = new Dictionary<string, object>();
                foreach (StationLoop loop in station.Loops)
                {
                    PDBTag standardTemperature = stationTags.FirstOrDefault(tag => tag.Name.Contains(station.AbbrName + "_" + loop.AbbrName + "_Temperature"));
                    PDBTag standardPressure = stationTags.FirstOrDefault(tag => tag.Name.Contains(station.AbbrName + "_" + loop.AbbrName + "_Pressure"));
                    if (standardTemperature != null && standardPressure != null)
                    {
                        if (!standardTemperature.Value.Contains("??") && !standardPressure.Value.Contains("??"))
                        {
                            standardTemperature.Value = "25"; standardPressure.Value = "101";
                            loopParameterStandard["LoopAbbrName"] = loop.AbbrName;
                            loopParameterStandard["StationAbbrName"] = loop.AbbrName;
                            loopParameterStandard["standardTemperature"] = Convert.ToDouble(standardTemperature.Value) == 25 ? true : false;
                            loopParameterStandard["standardPressure"] = Convert.ToDouble(standardPressure.Value) == 101 ? true : false;
                            loopParameterStandards.Add(loopParameterStandard);
                        }
                    }
                }
                station.StationStatistics["loopParameterStandards"] = loopParameterStandards;
                station.Loops = new List<StationLoop>();
                stationsData.Add(station);
            }
             rtn["Data"]= stationsData;
            return rtn;
        }
    }
}
