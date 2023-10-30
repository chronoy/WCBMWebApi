using Microsoft.AspNetCore.Mvc;
using Models;
using OfficeOpenXml;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MeteringCertificateController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly IEquipmentMeteringCertificateService _equipmentMeteringCertificateService;
        private readonly IExcelExportHelper _excelExportHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MeteringCertificateController(IEquipmentMeteringCertificateService equipmentMeteringCertificateService,
            IExcelExportHelper excelExportHelper,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            _equipmentMeteringCertificateService = equipmentMeteringCertificateService;
            _excelExportHelper = excelExportHelper;
            _Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentMeteringCertificates([FromForm] DateTime beginSearchDate, [FromForm] DateTime endSearchDate)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentMeteringCertificates = await _equipmentMeteringCertificateService.GetEquipmentMeteringCertificates(beginSearchDate, endSearchDate);
            if (equipmentMeteringCertificates == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentMeteringCertificates;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentMeteringCertificate([FromForm] EquipmentMeteringCertificate meteringCertificate)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            string resultString = await _equipmentMeteringCertificateService.AddEquipmentMeteringCertificate(meteringCertificate);
            rtn["MSG"] = resultString;
            switch (resultString)
            {
                case "OtherError":
                    rtn["Code"] = "400";
                    break;
                case "OK":
                    rtn["Code"] = "200";
                    break;
                case "NotExistEquipmentSerialNumber":
                    rtn["Code"] = "417";
                    break;
            }

            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentMeteringCertificate([FromForm] EquipmentMeteringCertificate meteringCertificate)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            string resultString = await _equipmentMeteringCertificateService.UpdateEquipmentMeteringCertificate(meteringCertificate);
            rtn["MSG"] = resultString;
            switch (resultString)
            {
                case "OtherError":
                    rtn["Code"] = "400";
                    break;
                case "OK":
                    rtn["Code"] = "200";
                    break;
                case "NotExistEquipmentSerialNumber":
                    rtn["Code"] = "417";
                    break;
            }

            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentMeteringCertificates([FromForm] List<int> ids)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            if (await _equipmentMeteringCertificateService.DeleteEquipmentMeteringCertificate(ids))
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

        [HttpPost]
        public async Task<Dictionary<string, object>> ExportExcelEquipmentMeteringCertificates([FromForm] DateTime beginSearchDate, [FromForm] DateTime endSearchDate)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            var equipmentMeteringCertificates = await _equipmentMeteringCertificateService.GetEquipmentMeteringCertificates(beginSearchDate, endSearchDate);
            string templatePath = Path.Combine(_hostingEnvironment.WebRootPath, @"ExcelTempate\计量设备证书.xlsx");
            byte[] filecontent = await _equipmentMeteringCertificateService.ExportEquipmentMeteringCertificates(equipmentMeteringCertificates, templatePath);
            rtn["Data"] = File(filecontent, _excelExportHelper.ExcelContentType, "计量设备证书.xlsx");
            rtn["Code"] = "200";
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> ImportEquipmentMeteringCertificates([FromForm(Name = "file")] List<IFormFile> files)
        {
            Dictionary<string, object> rtn = new();
            foreach (var file in files)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    string result = string.Empty;
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var package = new ExcelPackage(file.OpenReadStream());
                    var workbook = package.Workbook;

                    var meteringCertificates = await _excelExportHelper.ImportExcel<EquipmentMeteringCertificate>(workbook.Worksheets["证书信息"], 2, "2:2");
                    var meteringResultDatas = await _excelExportHelper.ImportExcel<EquipmentMeteringResultData>(workbook.Worksheets["检定校准结果"]);
                    var meteringCheckedDatas = await _excelExportHelper.ImportExcel<EquipmentMeteringCheckedData>(workbook.Worksheets["核验结果"]);
                    foreach (var certificate in meteringCertificates)
                    {
                        certificate.MeteringResultDatas = meteringResultDatas.Where(x => x.CertificateNumber == certificate.CertificateNumber).ToList();
                        certificate.MeteringCheckedDatas = meteringCheckedDatas.Where(x => x.CertificateNumber == certificate.CertificateNumber).ToList();
                        result = await _equipmentMeteringCertificateService.AddEquipmentMeteringCertificate(certificate);
                    }

                    rtn["MSG"] = result;
                    switch (result)
                    {
                        case "OtherError":
                            rtn["Code"] = "400";
                            break;
                        case "OK":
                            rtn["Code"] = "200";
                            break;
                        case "NotExistEquipmentSerialNumber":
                            rtn["Code"] = "417";
                            break;
                    }
                    rtn["Data"] = meteringCertificates;
                }
                else
                {
                    rtn["MSG"] = "文件格式错误，只支持xlsx格式Excel导入";
                    rtn["Code"] = "410";
                }
            }
            return rtn;
        }
    }
}
