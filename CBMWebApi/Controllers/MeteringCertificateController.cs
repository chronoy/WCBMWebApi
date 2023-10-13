using Microsoft.AspNetCore.Mvc;
using Models;
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
        public async Task<Dictionary<string, object>> GetEquipmentMeteringCertificates()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentMeteringCertificates = await _equipmentMeteringCertificateService.GetEquipmentMeteringCertificates();
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
                case "NotExistThisRecord":
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
    }
}
