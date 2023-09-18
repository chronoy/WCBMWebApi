using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentCompany()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentCompany = await _equipmentService.GetEquipmentParameters<EquipmentCompany>(x => true);
            if (equipmentCompany == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentCompany;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentCompany([FromForm] EquipmentCompany company)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentService.AddEquipmentParameter(company);
            switch (result)
            {
                case "OtherError":
                    rtn["MSG"] = result;
                    rtn["Code"] = "400";
                    break;
                case "OK":
                    rtn["MSG"] = result;
                    rtn["Code"] = "200";
                    break;
            }
            rtn["Data"] = company;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentCompany([FromForm] EquipmentCompany company)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentService.UpdateEquipmentParameter(company);
            if (!result)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = company;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentCompany([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentService.DeleteEquipmentParameterBy<EquipmentCompany>(x => x.ID == id);
            if (!result)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = id;
            return rtn;
        }
    }
}
