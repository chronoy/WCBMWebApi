using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Models;
using Services;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
        public async Task<Dictionary<string, object>> GetStations()
        {
            Dictionary<string, object> rtn = new();
            var stations = await _stationService.GetStations();
            if (stations == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = stations;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetStationLoops([FromForm] List<int> stationIDs)
        {
            Dictionary<string, object> rtn = new();
            List<StationLoop> loops = new List<StationLoop>();
            foreach (int stationID in stationIDs)
            {
                var stationLoops = await _stationLoopService.GetStationLoopsByStation(stationID);
                if (stationLoops != null)
                {
                    loops = loops.Union(stationLoops).ToList();
                }
            }
            if (loops.Count == 0)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = loops;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetStationEquipments([FromForm] List<int> stationIDs)
        {
            Dictionary<string, object> rtn = new();
            List<StationEquipment> equipments = new List<StationEquipment>();
            foreach (int stationID in stationIDs)
            {
                var stationEquipments = await _stationEquipmentService.GetStationEquipmentsBySttaion(stationID);
                if (stationEquipments != null)
                {
                    equipments = equipments.Union(stationEquipments).ToList();
                }
            }
            if (equipments.Count == 0)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipments;
            return rtn;
        }
    }
}
