﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoopFlowContrastController : ControllerBase
    {

        private readonly ILoopFlowContrastService _loopFlowContrastService;
        public LoopFlowContrastController(ILoopFlowContrastService loopFlowContrastService)
        {
            _loopFlowContrastService = loopFlowContrastService;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetLoopFlowContrastConfigs([FromForm] int stationID, [FromForm] List<int> contrastStates, [FromForm] DateTime beginDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var configs = await _loopFlowContrastService.GetLoopFlowContrastConfigs(stationID, contrastStates, beginDateTime, endDateTime);
            if (configs == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = configs;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddLoopFlowContrastConfig([FromForm] LoopFlowContrastConfig config)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _loopFlowContrastService.AddLoopFlowContrastConfig(config);
            switch (result)
            {
                case "OtherError":
                    rtn["MSG"] = result;
                    rtn["Code"] = "400";
                    break;

                case "OK":
                    rtn["MSG"] = result;
                    rtn["Code"] = "200";
                    break;
            }
            return rtn;
        }


        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateLoopFlowContrastConfig([FromForm] LoopFlowContrastConfig config)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            string resultString = await _loopFlowContrastService.UpdateLoopFlowContrastConfig(config);
            rtn["MSG"] = resultString;
            switch (resultString)
            {
                case "OtherError":
                    rtn["Code"] = "400";
                    break;
                case "OK":
                    rtn["Code"] = "200";
                    break;
            }

            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteLoopFlowContrastConfig([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            if (await _loopFlowContrastService.DeleteLoopFlowContrastConfig(id))
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            else
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            return rtn;
        }
    }
}

