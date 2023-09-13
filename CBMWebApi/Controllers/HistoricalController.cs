using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services;

namespace CBMCenterApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HistoricalController : Controller
    {
        private readonly IConfiguration _Configuration;
        private readonly IHistoricalTrendService _historicalTrendService;
        private readonly IExcelExportHelper _excelExportHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HistoricalController(IHistoricalTrendService historicalTrendService, IExcelExportHelper excelExportHelper, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _historicalTrendService = historicalTrendService;
            _excelExportHelper = excelExportHelper;
            _Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetHistoricalTrends([FromForm] int loopID, [FromForm] int trendGroupID, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string interval)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            string duration = endDateTime.Subtract(startDateTime).ToString(@"dd\:hh\:mm\:ss");
            var trendData =  await _historicalTrendService.GetHistoricalTrendsData(loopID, trendGroupID, startDateTime, interval, duration);
            if (trendData == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = trendData;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportHistoricalTrendsReport([FromForm] int loopID, [FromForm] int trendGroupID, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string interval)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            string duration = endDateTime.Subtract(startDateTime).ToString(@"dd\:hh\:mm\:ss");

            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\历史趋势报告.xlsx");
            byte[] filecontent = await _historicalTrendService.ExportHistoricalTrendsDataReport(loopID, trendGroupID, startDateTime, interval, duration, templatePath);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "历史趋势报告.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }
    }
}
