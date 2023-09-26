using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipmentParameterController : ControllerBase
    {
        private readonly IEquipmentParameterService _equipmentParameterService;

        public EquipmentParameterController(IEquipmentParameterService equipmentParameterService)
        {
            _equipmentParameterService = equipmentParameterService;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentCompany()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentCompany = await _equipmentParameterService.GetEquipmentParameters<EquipmentCompany>(x => true);
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

            var result = await _equipmentParameterService.AddEquipmentParameter(company);
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

            var result = await _equipmentParameterService.UpdateEquipmentParameter(company);
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

            var result = await _equipmentParameterService.DeleteEquipmentCompany(id);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentLine()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentLines = await _equipmentParameterService.GetEquipmentParameters<EquipmentLine>(x => true);
            if (equipmentLines == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentLines;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentLine([FromForm] EquipmentLine line)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.AddEquipmentParameter(line);
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
            rtn["Data"] = line;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentLine([FromForm] EquipmentLine line)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.UpdateEquipmentParameter(line);
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
            rtn["Data"] = line;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentLine([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.DeleteEquipmentParameterBy<EquipmentLine>(x => x.ID == id);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentStation([FromForm] int companyID)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentStations = await _equipmentParameterService.GetEquipmentParameters<EquipmentStation>(x => x.EquipmentCompanyID == companyID);
            if (equipmentStations == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentStations;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentStation([FromForm] EquipmentStation station)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.AddEquipmentParameter(station);
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
            rtn["Data"] = station;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentStation([FromForm] EquipmentStation station)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.UpdateEquipmentParameter(station);
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
            rtn["Data"] = station;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentStation([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.DeleteEquipmentParameterBy<EquipmentStation>(x => x.ID == id);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentCategory()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentCategories = await _equipmentParameterService.GetEquipmentParameters<EquipmentCategory>(x => true);
            if (equipmentCategories == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentCategories;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentCategory([FromForm] EquipmentCategory category)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.AddEquipmentParameter(category);
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
            rtn["Data"] = category;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentCategory([FromForm] EquipmentCategory category)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.UpdateEquipmentParameter(category);
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
            rtn["Data"] = category;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentCategory([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.DeleteEquipmentCategory(id);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentModel([FromForm] int categoryID, [FromForm] int manufacturerID)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentModels = await _equipmentParameterService.GetEquipmentParameters<EquipmentModel>(x => x.EquipmentCategoryID == categoryID && x.EquipmentManufacturerID == manufacturerID);
            if (equipmentModels == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentModels;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentModel([FromForm] EquipmentModel model)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.AddEquipmentParameter(model);
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
            rtn["Data"] = model;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentModel([FromForm] EquipmentModel model)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.UpdateEquipmentParameter(model);
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
            rtn["Data"] = model;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentModel([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.DeleteEquipmentParameterBy<EquipmentModel>(x => x.ID == id);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentAccuracy()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentAccuracies = await _equipmentParameterService.GetEquipmentParameters<EquipmentAccuracy>(x => true);
            if (equipmentAccuracies == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentAccuracies;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentAccuracy([FromForm] EquipmentAccuracy accuracy)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.AddEquipmentParameter(accuracy);
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
            rtn["Data"] = accuracy;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentAccuracy([FromForm] EquipmentAccuracy accuracy)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.UpdateEquipmentParameter(accuracy);
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
            rtn["Data"] = accuracy;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentAccuracy([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.DeleteEquipmentParameterBy<EquipmentAccuracy>(x => x.ID == id);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentPressure()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentPressures = await _equipmentParameterService.GetEquipmentParameters<EquipmentPressureLevel>(x => true);
            if (equipmentPressures == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentPressures;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentPressure([FromForm] EquipmentPressureLevel pressureLevel)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.AddEquipmentParameter(pressureLevel);
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
            rtn["Data"] = pressureLevel;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentPressure([FromForm] EquipmentPressureLevel pressureLevel)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.UpdateEquipmentParameter(pressureLevel);
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
            rtn["Data"] = pressureLevel;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentPressure([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.DeleteEquipmentParameterBy<EquipmentPressureLevel>(x => x.ID == id);
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

        [HttpPost]
        public async Task<Dictionary<string, object>> GetEquipmentManufacturer()
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var equipmentManufacturers = await _equipmentParameterService.GetEquipmentParameters<EquipmentManufacturer>(x => true);
            if (equipmentManufacturers == null)
            {
                rtn["MSG"] = "OtherError";
                rtn["Code"] = "400";
            }
            else
            {
                rtn["MSG"] = "OK";
                rtn["Code"] = "200";
            }
            rtn["Data"] = equipmentManufacturers;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> AddEquipmentManufacturer([FromForm] EquipmentManufacturer manufacturer)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.AddEquipmentParameter(manufacturer);
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
            rtn["Data"] = manufacturer;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> UpdateEquipmentManufacturer([FromForm] EquipmentManufacturer manufacturer)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.UpdateEquipmentParameter(manufacturer);
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
            rtn["Data"] = manufacturer;
            return rtn;
        }

        [HttpPost]
        public async Task<Dictionary<string, object>> DeleteEquipmentManufacturer([FromForm] int id)
        {
            Dictionary<string, object> rtn = new Dictionary<string, object>();

            var result = await _equipmentParameterService.DeleteEquipmentManufacturer(id);
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