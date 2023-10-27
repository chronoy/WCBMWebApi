using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParamterChangeController : ControllerBase
    {
        private readonly IKeyParameterService _service;
        public ParamterChangeController(IKeyParameterService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetKeyParameters([FromForm] List<int> loopIDs)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<KeyParameter> keyParameters = await _service.GetKeyParametersByLoop(loopIDs);
            if (keyParameters == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = keyParameters;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetKeyParametersChangeRecord([FromForm] List<int> loopIDs, [FromForm] DateTime beginTime, [FromForm] DateTime endTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<KeyParametersChangeRecord> records = await _service.GetKeyParametersChangeRecordByLoop(beginTime, endTime, loopIDs);
            if (records == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = records;
            return rtn;
        }
    }
}
