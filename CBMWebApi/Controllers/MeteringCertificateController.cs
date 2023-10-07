using Microsoft.AspNetCore.Mvc;
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
    }
}
