﻿using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly IAlarmService _alarmService;
        private readonly IExcelExportHelper _excelExportHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AlarmController(IAlarmService alarmService, IExcelExportHelper excelExportHelper, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _alarmService = alarmService;
            _excelExportHelper = excelExportHelper;
            _Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        
        [HttpPost]//yantao
        public async Task<Dictionary<string, object>> GetRealtimeAlarm([FromForm] List<string> alarmAreas, [FromForm] List<string> manufacturers, [FromForm] List<string> prioritys)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var realtimeAlarm = await _alarmService.GetRealtimeAlarm(alarmAreas, manufacturers, prioritys);
            if (realtimeAlarm == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = realtimeAlarm;
            return rtn;
        }


        [HttpPost]//yantao
        public async Task<Dictionary<string, object>> ACKRealtimeAlarm([FromForm] List<string> tagNames, [FromForm] string userName)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            string msg = await _alarmService.AckRealtimeAlarm(tagNames, userName);
            if (msg == "OtherError")
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            return rtn;
        }


        [HttpPost]
        public async Task<Dictionary<string, object>> ExportExcelRealtimeAlarm([FromForm] List<string> alarmAreas, [FromForm] List<string> manufacturers, [FromForm] List<string> prioritys)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var list = await _alarmService.GetRealtimeAlarm(alarmAreas, manufacturers ,prioritys);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\实时报警统计表.xlsx");
            string[] columnNames = _Configuration["RealtimeAlarmExportColumnNames"].ToString().Split(",");
            byte[] filecontent = await _excelExportHelper.ExportExcel(list.ToList(), columnNames, templatePath, 2, true);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "实时报警统计表.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetHistoricalAlarm([FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] List<string> alarmAreas, [FromForm] List<string> prioritys)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var historicalAlarms = await _alarmService.GetHistoricalAlarm(startDateTime, endDateTime, alarmAreas,prioritys);
            if (historicalAlarms == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = historicalAlarms;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportExcelHistoricalAlarm([FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] List<string> alarmAreas, [FromForm] List<string> prioritys)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var list = await _alarmService.GetHistoricalAlarm(startDateTime, endDateTime, alarmAreas, prioritys);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\历史报警统计表.xlsx");
            string[] columnNames = _Configuration["HistoricalAlarmExportColumnNames"].ToString().Split(",");
            byte[] filecontent = await _excelExportHelper.ExportExcel(list.ToList(), columnNames, templatePath, 2, true);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "历史报警统计表.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetHistoricalStatisticalAlarm([FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] List<string> alarmAreas, [FromForm] List<string> prioritys)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var statisticalAlarms = await _alarmService.GetHistoricalStatisticalAlarm(startDateTime, endDateTime, alarmAreas, prioritys);
            if (statisticalAlarms == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = statisticalAlarms;
            return rtn;
        }
    }
}
