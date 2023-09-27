using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReferenceMaterialController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly IReferenceMaterialCertificateService _referenceMaterialCertificateService;
        private readonly IExcelExportHelper _excelExportHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ReferenceMaterialController(IReferenceMaterialCertificateService referenceMaterialCertificateService,
            IExcelExportHelper excelExportHelper,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            _referenceMaterialCertificateService = referenceMaterialCertificateService;
            _excelExportHelper = excelExportHelper;
            _Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetReferenceMaterialCertificates([FromForm] DateTime beginSearchDate, [FromForm] DateTime endSearchDate)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var referenceMaterialCertificates = await _referenceMaterialCertificateService.GetReferenceMaterialCertificates(beginSearchDate, endSearchDate);
            if (referenceMaterialCertificates == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = referenceMaterialCertificates;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddReferenceMaterialCertificate([FromForm] ReferenceMaterialCertificate certificate)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            string resultString = await _referenceMaterialCertificateService.AddReferenceMaterialCertificate(certificate);
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
        public async Task<Dictionary<string, object>> UpdateReferenceMaterialCertificate([FromForm] ReferenceMaterialCertificate certificate)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            string resultString = await _referenceMaterialCertificateService.UpdateReferenceMaterialCertificate(certificate);
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
        public async Task<Dictionary<string, object>> DeleteReferenceMaterialCertificate([FromForm] List<int> ids)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();
            foreach (int id in ids)
            {
                if (await _referenceMaterialCertificateService.DeleteReferenceMaterialCertificateBy(x => x.ID == id))
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
    }
}
