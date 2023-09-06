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
        public async Task<IEnumerable<StationEquipmentCheckData>> CheckReport([FromForm] string vos)
        {

            return await _service.GetStationEquipmentCheckReport("",1,"",""); 
        }
    }
}
