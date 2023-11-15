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

        public List<Equipment> GetEquipments(string? companyId, string? lineId, string? stationId, string? categoryId, string? modelId, string? manufacturerId)
        {
            IEnumerable<Equipment> result = from e in _context.Equipments select e;

            if (!string.IsNullOrEmpty(companyId))
            {
                int[] companyIds = Array.ConvertAll(companyId.Split(','), int.Parse);
                if (!string.IsNullOrEmpty(stationId))
                {
                    int[] stationIds = Array.ConvertAll(stationId.Split(","), int.Parse);
                    string[] stations = (from c in _context.companies
                                         where companyIds.Contains(c.ID)
                                         join a in _context.Areas on c.ID equals a.CompanyID
                                         join s in _context.Stations on a.ID equals s.AreaID
                                         where stationIds.Contains(s.ID)
                                         select $"{c.Name}_{s.Name}").ToArray();
                    result = result.Where(x => stations.Contains($"{x.CompanyName}_{x.StationName}"));
                }
                else
                {
                    string[] stations = (from c in _context.companies
                                         where companyIds.Contains(c.ID)
                                         join a in _context.Areas on c.ID equals a.CompanyID
                                         join s in _context.Stations on a.ID equals s.AreaID
                                         select $"{c.Name}_{s.Name}").ToArray();
                    result = result.Where(x => stations.Contains($"{x.CompanyName}_{x.StationName}"));
                }
            }
            else
            {
                return new List<Equipment>();
            }

            if (!string.IsNullOrEmpty(lineId))
            {
                int[] lineIds = Array.ConvertAll(lineId.Split(','), int.Parse);
                string[] lines = _context.EquipmentLines.Where(x => lineIds.Contains(x.ID)).Select(s => s.Name).ToArray();
                result = result.Where(x => lines.Contains(x.LineName));
            }
            else
            {
                string[] lines = _context.EquipmentLines.Select(x => x.Name).ToArray();
                result = result.Where(x => lines.Contains(x.LineName));
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                int[] categoryIds = Array.ConvertAll(categoryId.Split(','), int.Parse);
                string[] categories = _context.EquipmentCategories.Where(x => categoryIds.Contains(x.ID)).Select(x => x.Name).ToArray();
                result = result.Where(x => categories.Contains(x.Category));
            }
            else
            {
                string[] categories = _context.EquipmentCategories.Select(x => x.Name).ToArray();
                result = result.Where(x => categories.Contains(x.Category));
            }

            if (!string.IsNullOrEmpty(manufacturerId))
            {
                int[] manufacturerIds = Array.ConvertAll(manufacturerId.Split(','), int.Parse);
                string[] manufacturers = _context.EquipmentManufacturers.Where(x => manufacturerIds.Contains(x.ID)).Select(s => s.Name).ToArray();
                result = result.Where(x => manufacturers.Contains(x.Manufacturer));
            }
            else
            {
                string[] manufacturers = _context.EquipmentManufacturers.Select(s => s.Name).ToArray();
                result = result.Where(x => manufacturers.Contains(x.Manufacturer));
            }

            if (!string.IsNullOrEmpty(modelId))
            {
                int[] modelIds = Array.ConvertAll(modelId.Split(','), int.Parse);
                string[] models = (from model in _context.EquipmentModels
                                   where modelIds.Contains(model.ID)
                                   join m in _context.EquipmentManufacturers on model.EquipmentManufacturerID equals m.ID
                                   join c in _context.EquipmentCategories on model.EquipmentCategoryID equals c.ID
                                   select $"{c.Name}_{m.Name}_{model.Name}").ToArray();
                result = result.Where(x => models.Contains($"{x.Category}_{x.Manufacturer}_{x.EquipmentModel}"));
            }
            else
            {
                int[] categoryIds = _context.EquipmentCategories.Select(s => s.ID).ToArray();
                int[] manufacturerIds = _context.EquipmentManufacturers.Select(s => s.ID).ToArray();
                int[] modelIds = _context.EquipmentModels.Where(x => categoryIds.Contains(x.EquipmentCategoryID) && manufacturerIds.Contains(x.EquipmentManufacturerID)).Select(s => s.ID).ToArray();
                string[] models = (from model in _context.EquipmentModels
                                   where modelIds.Contains(model.ID)
                                   join m in _context.EquipmentManufacturers on model.EquipmentManufacturerID equals m.ID
                                   join c in _context.EquipmentCategories on model.EquipmentCategoryID equals c.ID
                                   select $"{c.Name}_{m.Name}_{model.Name}").ToArray();
                result = result.Where(x => models.Contains($"{x.Category}_{x.Manufacturer}_{x.EquipmentModel}"));
            }

            return result.ToList();
        }

        public List<string> GetEquipmentSerialNumbers()
        {
            IEnumerable<Equipment> result = from e in _context.Equipments select e;

            return result.GroupBy(g => g.SerialNumber).Select(s => s.Key).ToList();
        }

        public List<Equipment> GetEquipmentInfo(List<string> serialNumbers)
        {
            IEnumerable<Equipment> result = from e in _context.Equipments
                                            where serialNumbers.Contains(e.SerialNumber)
                                            select e;

            return result.ToList();
        }

        public bool ValidSerialNumber(string serialNumber)
        {
            return !_context.Equipments.Where(s => s.SerialNumber == serialNumber).Any();
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
