using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static iText.IO.Util.IntHashtable;

namespace Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRespository _equipmentRespository;
        public EquipmentService(IEquipmentRespository equipmentRespository)
        {
            _equipmentRespository = equipmentRespository;
        }

        public Task<List<Equipment>> GetEquipments(string? company, string? line, string? station, string? category, string? model, string? accuracy, string? pressure, string? manufacturer)
        {
            return Task.Run(() => _equipmentRespository.GetEquipments(company, line, station, category, model, accuracy, pressure, manufacturer));
        }

        public Task<string> AddEquipment(Equipment entity)
        {
            return Task.Run(() => _equipmentRespository.AddEquipment(entity));
        }

        public Task<string> AddEquipments(List<Equipment> entities)
        {
            return Task.Run(() => _equipmentRespository.AddEquipments(entities));
        }

        public Task<string> UpdateEquipment(Equipment entity)
        {
            return Task.Run(() => _equipmentRespository.UpdateEquipment(entity));
        }

        public Task<bool> DeleteEquipment(int id)
        {
            return Task.Run(() => _equipmentRespository.DeleteEquipment(id));
        }
    }
}
