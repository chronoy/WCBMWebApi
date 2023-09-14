using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Models;
using Services;
using System.Diagnostics.CodeAnalysis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;
        private readonly IStationLoopService _stationLoopService;
        private readonly IStationEquipmentService _stationEquipmentService;
        private readonly IPDBService _PDBService;
        public StationController(IStationService stationService,
                                 IStationLoopService stationLoopService,
                                 IStationEquipmentService stationEquipmentService,
                                 IPDBService PDBService
                                 )
        {
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
            List<List<PDBTag>> loopTags = new List<List<PDBTag>>();
            List<List<PDBTag>> EquipmentTags = new List<List<PDBTag>>();
            foreach (StationLoop loop in loops)
            {
                loopTags.Add(stationTags.Where(tag => tag.Name.Split('_')[1] == loop.AbbrName).ToList());
            }
            foreach (StationEquipment equipment in equipments)
            {
                EquipmentTags.Add(stationTags.Where(tag => tag.Name.Split('_')[1] == equipment.AbbrName).ToList());
            }


            rtn["Loops"] = loops;
            rtn["Equipments"] = equipments;
            rtn["LoopTags"] = loopTags;
            rtn["EquipmentTags"] = EquipmentTags;
            return rtn;
        }
    }
}
