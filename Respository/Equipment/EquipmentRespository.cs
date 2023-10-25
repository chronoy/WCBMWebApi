using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class EquipmentRespository : BaseClass, IEquipmentRespository
    {
        private readonly SQLServerDBContext _context;
        public EquipmentRespository(SQLServerDBContext context) : base(context)
        {
            _context = context;
        }

        public List<Equipment> GetEquipments(string? company, string? line, string? station, string? category, string? model, string? manufacturer)
        {
            IEnumerable<Equipment> result = from e in _context.Equipments select e;

            if (!string.IsNullOrEmpty(company))
            {
                string[] companies = company.Split(",");
                result = result.Where(x => companies.Contains(x.CompanyName));
                if (!string.IsNullOrEmpty(station))
                {
                    string[] stations = station.Split(",");
                    result = result.Where(x => stations.Contains(x.StationName));
                }
                else
                {
                    int[] companyIds = _context.EquipmentCompanies.Where(x => companies.Contains(x.Name)).Select(s => s.ID).ToArray();
                    string[] stations = _context.EquipmentStations.Where(x => companyIds.Contains(x.EquipmentCompanyID)).Select(s => s.Name).ToArray();
                    result = result.Where(x => stations.Contains(x.StationName));
                }
            }
            else
            {
                int[] companyIds = _context.EquipmentCompanies.Select(s => s.ID).ToArray();
                string[] companies = _context.EquipmentCompanies.Select(s => s.Name).ToArray();
                string[] stations = _context.EquipmentStations.Where(x => companyIds.Contains(x.EquipmentCompanyID)).Select(s => s.Name).ToArray();
                result = result.Where(x => companies.Contains(x.CompanyName) && stations.Contains(x.StationName));
            }

            if (!string.IsNullOrEmpty(line))
            {
                string[] lines = line.Split(",");
                result = result.Where(x => lines.Contains(x.LineName));
            }
            else
            {
                string[] lines = _context.EquipmentLines.Select(x => x.Name).ToArray();
                result = result.Where(x => lines.Contains(x.LineName));
            }

            if (!string.IsNullOrEmpty(category))
            {
                string[] categories = category.Split(",");
                result = result.Where(x => categories.Contains(x.Category));
            }
            else
            {
                string[] categories = _context.EquipmentCategories.Select(x => x.Name).ToArray();
                result = result.Where(x => categories.Contains(x.Category));
            }

            if (!string.IsNullOrEmpty(manufacturer))
            {
                string[] manufacturers = manufacturer.Split(",");
                result = result.Where(x => manufacturers.Contains(x.Manufacturer));
            }
            else
            {
                string[] manufacturers = _context.EquipmentManufacturers.Select(s => s.Name).ToArray();
                result = result.Where(x => manufacturers.Contains(x.Manufacturer));
            }

            if (!string.IsNullOrEmpty(manufacturer) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(model))
            {
                string[] models = model.Split(",");
                result = result.Where(x => models.Contains(x.EquipmentModel));
            }
            else
            {
                int[] categoryIds = _context.EquipmentCategories.Select(s => s.ID).ToArray();
                int[] manufacturerIds = _context.EquipmentManufacturers.Select(s => s.ID).ToArray();
                string[] categories = _context.EquipmentCategories.Select(x => x.Name).ToArray();
                string[] manufacturers = _context.EquipmentManufacturers.Select(s => s.Name).ToArray();
                string[] models = _context.EquipmentModels.Where(x => categoryIds.Contains(x.EquipmentCategoryID) && manufacturerIds.Contains(x.EquipmentManufacturerID)).Select(s => s.Name).ToArray();
                result = result.Where(x => categories.Contains(x.Category) && manufacturers.Contains(x.Manufacturer) && models.Contains(x.EquipmentModel));
            }

            return result.ToList();
        }

        public string AddEquipment(Equipment entity)
        {
            entity.EnterDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            return AddEntity(entity);
        }

        public string AddEquipments(List<Equipment> entities)
        {
            entities.ForEach(e => { e.EnterDate = DateTime.Now; e.UpdateDate = DateTime.Now; });
            return AddEntitys(entities);
        }

        public string UpdateEquipment(Equipment entity)
        {
            var equipment = _context.Equipments.FirstOrDefault(x => x.ID == entity.ID);
            if (equipment != null)
            {
                equipment.UpdateDate = DateTime.Now;
                equipment.LineName = entity.LineName;
                equipment.CompanyName = entity.CompanyName;
                equipment.StationName = entity.StationName;
                equipment.Category = entity.Category;
                equipment.Manufacturer = entity.Manufacturer;
                equipment.EquipmentModel = entity.EquipmentModel;
                equipment.Customer = entity.Customer;
                equipment.ProcessLocation = entity.ProcessLocation;
                equipment.SerialNumber = entity.SerialNumber;
                equipment.Caliber = entity.Caliber;
                equipment.Range = entity.Range;
                equipment.Accuracy = entity.Accuracy;
                equipment.Uncertainty = entity.Uncertainty;
                equipment.PressureLevel = entity.PressureLevel;
                equipment.InsideDiameter = entity.InsideDiameter;
                equipment.Length = entity.Length;
                equipment.KFactor = entity.KFactor;
                equipment.CommonFlow = entity.CommonFlow;
                equipment.InstallationCondition = entity.InstallationCondition;
                equipment.ProductionDate = entity.ProductionDate;
                equipment.Status = entity.Status;
                equipment.TradeProperty = entity.TradeProperty;
                equipment.VerificationEndDate = entity.VerificationEndDate;
                equipment.VerificationPeriod = entity.VerificationPeriod;
                equipment.VerificationAgency = entity.VerificationAgency;
                equipment.VerificationCertificateNumber = entity.VerificationCertificateNumber;
                equipment.MaintenanceStatus = entity.MaintenanceStatus;
                equipment.DesignDrawings = entity.DesignDrawings;
                equipment.Note = entity.Note;
                return UpdateEntity(equipment);
            }
            else
            {
                return "NotExistThisRecord";
            }
        }

        public bool UpdateEquipments(List<Equipment> listEntity)
        {
            List<Equipment> equipments = new List<Equipment>();
            listEntity.ForEach(e =>
            {
                var equipment = _context.Equipments.FirstOrDefault(x => x.ID == e.ID);
                if (equipment != null)
                {
                    equipment.UpdateDate = DateTime.Now;
                    equipment.LineName = e.LineName;
                    equipment.CompanyName = e.CompanyName;
                    equipment.StationName = e.StationName;
                    equipment.Category = e.Category;
                    equipment.Manufacturer = e.Manufacturer;
                    equipment.EquipmentModel = e.EquipmentModel;
                    equipment.Customer = e.Customer;
                    equipment.ProcessLocation = e.ProcessLocation;
                    equipment.SerialNumber = e.SerialNumber;
                    equipment.Caliber = e.Caliber;
                    equipment.Range = e.Range;
                    equipment.Accuracy = e.Accuracy;
                    equipment.Uncertainty = e.Uncertainty;
                    equipment.PressureLevel = e.PressureLevel;
                    equipment.InsideDiameter = e.InsideDiameter;
                    equipment.Length = e.Length;
                    equipment.KFactor = e.KFactor;
                    equipment.CommonFlow = e.CommonFlow;
                    equipment.InstallationCondition = e.InstallationCondition;
                    equipment.ProductionDate = e.ProductionDate;
                    equipment.Status = e.Status;
                    equipment.TradeProperty = e.TradeProperty;
                    equipment.VerificationEndDate = e.VerificationEndDate;
                    equipment.VerificationPeriod = e.VerificationPeriod;
                    equipment.VerificationAgency = e.VerificationAgency;
                    equipment.VerificationCertificateNumber = e.VerificationCertificateNumber;
                    equipment.MaintenanceStatus = e.MaintenanceStatus;
                    equipment.DesignDrawings = e.DesignDrawings;
                    equipment.Note = e.Note;
                    equipments.Add(equipment);
                }
            });
            return UpdateEntitys(equipments) == "OK";
        }

        public bool DeleteEquipment(int id)
        {
            return DeleteEntityBy<Equipment>(x => x.ID == id);
        }
    }
}
