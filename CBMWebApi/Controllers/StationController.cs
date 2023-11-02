using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Models;
using Services;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
            List<StationEquipment> equipments = await _stationEquipmentService.GetStationEquipmentsByStation(stationID);
            List<AlarmCount> alarmCounts = await _alarmService.GetAlarmCountByStation(station);
            List<StationLoopDiagnosticData> loopDiagnosticDatas = await _diagnosisService.GetLoopDiagnosticDataByStation(station.ID);
            List<StationEquipmentDiagnosticData> equipmentDiagnosticDatas = await _diagnosisService.GetEquipmentDiagnosticDataByStation(station.ID);
            List<List<PDBTag>> loopTags = new List<List<PDBTag>>();
            List<Dictionary<string, bool>> loopConditions = new List<Dictionary<string, bool>>();
            List<List<PDBTag>> EquipmentTags = new List<List<PDBTag>>();
            List<List<AlarmCount>> loopAlarmCount = new List<List<AlarmCount>>();
            List<List<AlarmCount>> equipmentAlarmCount = new List<List<AlarmCount>>();
            foreach (StationLoop loop in loops)
            {
                loopTags.Add(stationTags.Where(tag => tag.Name.Split('_')[1] == loop.AbbrName).ToList());
                loopAlarmCount.Add(alarmCounts.Where(x => x.Name == loop.AbbrName).ToList());
                if (loop.EquipmentCategoryName == "超声流量计")
                {
                    Dictionary<string, bool> loopConditon = new Dictionary<string, bool>();
                    string temp = loopTags[loopTags.Count - 1].Single(tag => tag.Name.Contains("TemperatureCondition")).Value;
                    float temperrature = 0;
                    if (float.TryParse(temp, out temperrature))
                    {
                        if (temperrature == 20)
                        {
                            loopConditon["TemperatureConditon"] = false;
                        }
                        else
                        {
                            loopConditon["TemperatureConditon"] = true;
                        }
                    }
                    else
                    {
                        loopConditon["TemperatureConditon"] = false;
                    }
                    string pres = loopTags[loopTags.Count - 1].Single(tag => tag.Name.Contains("PressureCondition")).Value;
                    float pressure = 0;
                    if (float.TryParse(pres, out pressure))
                    {
                        if (pressure == 101.325)
                        {
                            loopConditon["PressureConditon"] = false;
                        }
                        else
                        {
                            loopConditon["PressureConditon"] = true;
                        }
                    }
                    else
                    {
                        loopConditon["PressureConditon"] = false;
                    }
                    loopConditions.Add(loopConditon);
                }
                else
                {
                    Dictionary<string, bool> loopConditon= new Dictionary<string, bool>();
                    loopConditon["TemperatureConditon"] = false;
                    loopConditon["PressureConditon"] = false;
                    loopConditions.Add(loopConditon);
                }
            }
            foreach (StationEquipment equipment in equipments)
            {
                EquipmentTags.Add(stationTags.Where(tag => tag.Name.Split('_')[1] == equipment.AbbrName).ToList());
                equipmentAlarmCount.Add(alarmCounts.Where(x => x.Name == equipment.AbbrName).ToList());
            }


            rtn["Loops"] = loops;
            rtn["Equipments"] = equipments;
            rtn["LoopTags"] = loopTags;
            rtn["LoopCondition"]= loopConditions;
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
    }
}
