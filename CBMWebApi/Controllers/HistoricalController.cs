using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services;
using static System.Collections.Specialized.BitVector32;

namespace CBMCenterApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HistoricalController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly IStationService _stationService;
        private readonly IStationLoopService _stationLoopService;
        private readonly IHistoricalTrendService _historicalTrendService;
        private readonly IExcelExportHelper _excelExportHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HistoricalController(IHistoricalTrendService historicalTrendService,
            IStationService stationService,
            IStationLoopService stationLoopService,
            IExcelExportHelper excelExportHelper,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            _stationService = stationService;
            _stationLoopService = stationLoopService;
            _historicalTrendService = historicalTrendService;
            _excelExportHelper = excelExportHelper;
            _Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetHistoricalTrends([FromForm] List<string> trendTags, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string interval)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            string duration = endDateTime.Subtract(startDateTime).ToString(@"dd\:hh\:mm\:ss");
            var trendData = await _historicalTrendService.GetHistoricalTrendsData(trendTags, startDateTime, interval, duration);
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
        public async Task<Dictionary<string, object>> ExportHistoricalTrendsReport([FromForm] List<string> trendTags, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime, [FromForm] string interval)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            string duration = endDateTime.Subtract(startDateTime).ToString(@"dd\:hh\:mm\:ss");

            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\历史趋势报告.xlsx");
            byte[] filecontent = await _historicalTrendService.ExportHistoricalTrendsDataReport(trendTags, startDateTime, interval, duration, templatePath);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "历史趋势报告.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetHistoricalTrendFilterItems()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            List<Station> stations = await _stationService.GetStations();
            List<StationLoop> loops = await _stationLoopService.GetStationLoops();
            List<TrendTag> trendTags = await _historicalTrendService.GetTrendTags();

            rtn["stations"] = stations.Select(s => new { s.ID, s.Name, s.AbbrName });
            rtn["Loops"] = loops.Select(s => new { s.ID, s.Name, s.AbbrName, s.StationID }); 
            rtn["TrendTags"] = trendTags.Select(s => new { s.Address, s.Description, s.LoopID });
            return rtn;
        }
    }
}
