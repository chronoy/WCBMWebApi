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

        // GET: api/<CheckController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CheckController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CheckController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CheckController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CheckController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
    }
}
