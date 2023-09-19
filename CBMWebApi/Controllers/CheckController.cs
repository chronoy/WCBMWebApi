using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly ICheckService _service;

        public CheckController(ICheckService checkService)
        {
            _service = checkService;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> EquipmentCheckReport([FromForm] string reportCategory, [FromForm] string brandName, [FromForm] int equipmentID, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<HistoricalStationEquipmentCheckData> stationEquipmentCheckData = await _service.GetStationEquipmentCheckReport(reportCategory, brandName, equipmentID, startDateTime, endDateTime);
            if (stationEquipmentCheckData == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = stationEquipmentCheckData;
            return rtn;
        }
        [HttpPost]
        public async Task<Dictionary<string, object>> LoopCheckReport([FromForm] string reportCategory, [FromForm] string brandName, [FromForm] int loopID, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<HistoricalStationLoopCheckData> stationLoopCheckData = await _service.GetStationLoopCheckReport(reportCategory, brandName, loopID, startDateTime, endDateTime);
            if (stationLoopCheckData == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            } else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = stationLoopCheckData;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetManualCheckData([FromForm] int loopID , [FromForm] string brandName)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            await _service.GetManualCheckData(loopID, brandName);
            return data;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetOfflineCheck([FromForm] OfflineCheck offlineCheck)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            return await _service.GetOfflineCheck(offlineCheck);
        }
    }
}
