using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlarmController : Controller
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetRealtimeAlarm([FromForm] string alarmArea, [FromForm] string priority)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var realtimeAlarm = await _alarmService.GetRealtimeAlarm(alarmArea, priority);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportExcelRealtimeAlarm([FromForm] string alarmArea, [FromForm] string priority)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var list = await _alarmService.GetRealtimeAlarm(alarmArea, priority);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\实时报警统计表.xlsx");
            string[] columnNames = _Configuration["RealtimeAlarmExportColumnNames"].ToString().Split(",");
            byte[] filecontent = await _excelExportHelper.ExportExcel(list.ToList(), columnNames, templatePath, 2, true);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "实时报警统计表.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetRealtimeDiagnosticAlarm([FromForm] int stationID, [FromForm] int loopID)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var realtimeDiagnosticAlarm = await _alarmService.GetRealtimeDiagnosticAlarm(stationID, loopID);
            if (realtimeDiagnosticAlarm == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = realtimeDiagnosticAlarm;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportExcelRealtimeDiagnosticAlarm([FromForm] int stationID, [FromForm] int loopID)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var list = await _alarmService.GetRealtimeDiagnosticAlarm(stationID, loopID);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\实时诊断报警统计表.xlsx");
            string[] columnNames = _Configuration["RealtimeDiagnosticAlarmExportColumnNames"].ToString().Split(",");
            byte[] filecontent = await _excelExportHelper.ExportExcel(list.ToList(), columnNames, templatePath, 2, true);
            rtn["Data"] =  File(filecontent, _excelExportHelper.ExcelContentType, "实时诊断报警统计表.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetHistoricalAlarm([FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string alarmArea)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var historicalAlarm = await _alarmService.GetHistoricalAlarm(startDateTime, endDateTime, alarmArea);
            if (historicalAlarm == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = historicalAlarm;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportExcelHistoricalAlarm([FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string alarmArea)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var list = await _alarmService.GetHistoricalAlarm(startDateTime, endDateTime, alarmArea);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\历史报警统计表.xlsx");
            string[] columnNames = _Configuration["HistoricalAlarmExportColumnNames"].ToString().Split(",");
            byte[] filecontent = await _excelExportHelper.ExportExcel(list.ToList(), columnNames, templatePath, 2, true);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "历史报警统计表.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }


        [HttpPost]
        public async Task<Dictionary<string, object>> GetHistoricalAlarmKPI([FromForm] int topNumber, [FromForm] string sortType, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string alarmArea)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var historicalAlarmKPI = await _alarmService.GetHistoricalAlarmKPI(topNumber, sortType, startDateTime, endDateTime, alarmArea);
            if (historicalAlarmKPI == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = historicalAlarmKPI;
            return rtn;
        }
    }
}
