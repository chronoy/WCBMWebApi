using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Models;
using Newtonsoft.Json;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IProductionReportService _productionReportService;
        private readonly IConfiguration _configuration;
        private readonly IExcelExportHelper _excelExportHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ReportController(IProductionReportService productionReportService, IExcelExportHelper excelExportHelper, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _productionReportService = productionReportService;
            _excelExportHelper = excelExportHelper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetProductionReport([FromForm] string loopIDs, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var productionReport = await _productionReportService.GetProductionReportData(loopIDs, startDateTime, endDateTime);
            if (productionReport == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = productionReport;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportExcelProductionReport([FromForm] string loopIDs, [FromForm] DateTime startDateTime, [FromForm] DateTime endDateTime)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var ProductionReportlist = await _productionReportService.GetProductionReportData(loopIDs, startDateTime, endDateTime);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\能量报告.xlsx");
            string[] columnNames = _configuration["ProductionReportExportColumnNames"].ToString().Split(",");
            byte[] filecontent = await _excelExportHelper.ExportExcel(ProductionReportlist.ToList(), columnNames, templatePath, 2, true);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "能量报告.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }
    }
}
