using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
        public async Task<Dictionary<string, object>> EquipmentCheckReport([FromForm] string reportCategory, [FromForm] string manufacturer, [FromForm] int equipmentID, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<HistoricalStationEquipmentCheckData> stationEquipmentCheckData = await _service.GetStationEquipmentCheckReport(reportCategory, manufacturer, equipmentID, startDateTime, endDateTime);
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
        public async Task<Dictionary<string, object>> LoopCheckReport([FromForm] string reportCategory, [FromForm] string manufacturer, [FromForm] int loopID, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<HistoricalStationLoopCheckData> stationLoopCheckData = await _service.GetStationLoopCheckReport(reportCategory, manufacturer, loopID, startDateTime, endDateTime);
            if (stationLoopCheckData == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = stationLoopCheckData;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetManualCheckData([FromForm] int loopID, [FromForm] string manufacturer)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var data = await _service.GetManualCheckData(loopID, manufacturer);
            if (data == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = data;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetOfflineCheck([FromForm] OfflineCheck offlineCheck)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var data = await _service.GetOfflineCheck(offlineCheck);
            if (data.Values.Count == 0)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = data;
            return rtn;
        }
        [HttpPost]
        public async Task<Dictionary<string, object>> GetOnlineGCRepeatabilityCheck([FromForm] int ID, [FromForm] List<Data> firstDatas, [FromForm] List<Data> secondDatas)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var data = await _service.GetOnlineGCRepeatabilityCheck(ID, firstDatas, secondDatas);
            if (data == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = data;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetGCUnnormalizedComponentsCheck([FromForm] int ID, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string interval)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            string duration = endDateTime.Subtract(startDateTime).ToString(@"dd\:hh\:mm\:ss");
            var data = await _service.GetGCUnnormalizedComponentsCheck(ID,startDateTime,interval,duration);
            if (data == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = data;
            return rtn;
        }
    }
}
