using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Models;
using Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Collections.Specialized.BitVector32;

namespace CBMCenterApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEquipmentService _equipmentService;
        private readonly IExcelExportHelper _excelExportHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EquipmentController(IConfiguration configuration, IEquipmentService equipmentService, IExcelExportHelper excelExportHelper, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _equipmentService = equipmentService;
            _excelExportHelper = excelExportHelper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipments([FromForm] string? company, [FromForm] string? line, [FromForm] string? station,
                                                              [FromForm] string? category, [FromForm] string? model, [FromForm] string? manufacturer)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipments = await _equipmentService.GetEquipments(company, line, station, category, model, manufacturer);
            if (equipments == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipments;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipment([FromForm] Equipment equipment)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            string resultString = await _equipmentService.AddEquipment(equipment);
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
        public async Task<Dictionary<string, object>> UpdateEquipment([FromForm] Equipment equipment)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            string resultString = await _equipmentService.UpdateEquipment(equipment);
            rtn["MSG"] = resultString;
            switch (resultString)
            {
                case "OtherError":
                    rtn["Code"] = "400";
                    break;
                case "OK":
                    rtn["Code"] = "200";
                    break;
                case "NotExistThisRecord":
                    rtn["Code"] = "417";
                    break;
            }

            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipment([FromForm] List<int> ids)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            foreach (int id in ids)
            {
                if (await _equipmentService.DeleteEquipment(id))
                {
                    rtn["MSG"] = "OK";
                    rtn["Code"] = "200";
                }
                else
                {
                    rtn["MSG"] = "OtherError";
                    rtn["Code"] = "400";
                }
            }
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportEquipments([FromForm] string? company, [FromForm] string? line, [FromForm] string? station,
                                                              [FromForm] string? category, [FromForm] string? model, [FromForm] string? manufacturer)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var ProductionReportlist = await _equipmentService.GetEquipments(company, line, station, category, model, manufacturer);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\计量设备信息表.xlsx");
            string[] columnNames = _configuration["EquipmentExportColumnNames"].ToString().Split(",");
            byte[] filecontent = await _excelExportHelper.ExportExcel(ProductionReportlist.ToList(), columnNames, templatePath, 3, true);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "计量设备信息表.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ImportEquipments([FromForm(Name = "file")] List<IFormFile> files)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            foreach (var file in files)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    var equipments = await _excelExportHelper.ImportExcel<Equipment>(file, 2, "2:2");

                    string result = await _equipmentService.AddEquipments(equipments);
                    rtn["MSG"] = result;
                    switch (result)
                    {
                        case "OtherError":
                            rtn["Code"] = "400";
                            break;
                        case "OK":
                            rtn["Code"] = "200";
                            break;
                    }
                    rtn["Data"] = equipments;
                }
                else
                {
                    rtn["MSG"] = "文件格式错误，只支持xlsx格式Excel导入";
                    rtn["Code"] = "410";
                }
            }
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetPDFTextTest([FromForm(Name = "file")] List<IFormFile> files)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            foreach (var file in files)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".pdf")
                {
                    var text = await _excelExportHelper.GetPDFText(file);
                    if (text == null)
                    {
                        rtn["MSG"] = "OK";
                        rtn["Code"] = "200";
                    }
                    else
                    {
                        rtn["MSG"] = "OK";
                        rtn["Code"] = "200";
                    }
                    rtn["Data"] = text;
                }
                else
                {
                    rtn["MSG"] = "文件格式错误，请选择pdf格式";
                    rtn["Code"] = "410";
                }
            }
            return rtn;
        }
    }
}