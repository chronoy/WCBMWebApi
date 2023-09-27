using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PDBController : ControllerBase
    {
        private readonly IPDBService _service;
        private readonly IStationEquipmentService _equipmentServices;
        public PDBController(IPDBService PDBService, IStationEquipmentService equipmentService)
        {
            _service = PDBService;
            _equipmentServices = equipmentService;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetGCComponentByID([FromForm] int ID)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            StationEquipment equipment = await _equipmentServices.GetStationEquipmentByID(ID);
            List<PDBTag> components = await _service.GetGCComponent(equipment);
            if (components == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = components;
            return rtn;
        }
    }
}
