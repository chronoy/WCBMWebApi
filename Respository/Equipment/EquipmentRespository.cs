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
    public class EquipmentRespository : IEquipmentRespository
    {
        private readonly SQLServerDBContext _context;
        public EquipmentRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<Equipment> GetEquipments(string? company, string? line, string? station, string? category, string? model, string? accuracy, string? pressure, string? manufacturer)
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

            if (!string.IsNullOrEmpty(accuracy))
            {
                string[] accuracies = accuracy.Split(",");
                result = result.Where(x => accuracies.Contains(x.Accuracy));
            }
            else
            {
                string[] accuracies = _context.EquipmentAccuracies.Select(s => s.Name).ToArray();
                result = result.Where(x => accuracies.Contains(x.Accuracy));
            }

            if (!string.IsNullOrEmpty(pressure))
            {
                string[] pressures = pressure.Split(",");
                result = result.Where(x => pressures.Contains(x.PressureLevel));
            }
            else
            {
                string[] pressures = _context.EquipmentPressureLevels.Select(s => s.Name).ToArray();
                result = result.Where(x => pressures.Contains(x.PressureLevel));
            }

            return result.ToList();
        }

        public string AddEquipment(Equipment entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                entity.EnterDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                _context.Equipments.AddRange(entity);
                _context.SaveChanges();
                _context.Entry(entity);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string AddEquipments(List<Equipment> entities)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                entities.ForEach(e => { e.EnterDate = DateTime.Now; e.UpdateDate = DateTime.Now; });
                _context.Equipments.AddRange(entities);
                _context.SaveChanges();
                entities.ForEach(e => _context.Entry(e));
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string UpdateEquipment(Equipment entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                entity.UpdateDate = DateTime.Now;
                _context.Equipments.Update(entity);
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return "OtherError";
            }
            return "OK";
        }

        public bool UpdateEquipments<T>(List<Equipment> listEntity)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                listEntity.ForEach(e => e.UpdateDate = DateTime.Now);
                _context.Equipments.UpdateRange(listEntity);
                result = _context.SaveChanges() > 0;
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return result;
            }
            return result;
        }

        public bool DeleteEquipment(int id)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<Equipment> listDeleting = _context.Equipments.Where(x => x.ID == id).ToList();
                listDeleting.ForEach(u =>
                {
                    _context.Equipments.Attach(u);
                    _context.Equipments.Remove(u);
                });
                result = _context.SaveChanges() > 0;
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return result;
            }
            return result;
        }
    }
}
