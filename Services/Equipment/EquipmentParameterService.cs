using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EquipmentParameterService : IEquipmentParameterService
    {
        private readonly IEquipmentParameterRespository _equipmentParameterRespository;
        public EquipmentParameterService(IEquipmentParameterRespository equipmentParameterRespository)
        {
            _equipmentParameterRespository = equipmentParameterRespository;
        }
        public Task<List<T>> GetEquipmentParameters<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return Task.Run(() => _equipmentParameterRespository.GetEquipmentParameters(whereLambda));
        }

        public Task<string> AddEquipmentParameter<T>(T entity) where T : class
        {
            return Task.Run(() => _equipmentParameterRespository.AddEquipmentParameter(entity));
        }

        public Task<string> AddEquipmentParameters<T>(List<T> entities) where T : class
        {
            return Task.Run(() => _equipmentParameterRespository.AddEquipmentParameters(entities));
        }

        public Task<bool> UpdateEquipmentParameter<T>(T entity) where T : class
        {
            return Task.Run(() => _equipmentParameterRespository.UpdateEquipmentParameter(entity));
        }

        public Task<bool> UpdateEquipmentParameters<T>(List<T> listEntity) where T : class
        {
            return Task.Run(() => _equipmentParameterRespository.UpdateEquipmentParameters(listEntity));
        }

        public Task<bool> DeleteEquipmentParameter<T>(T entity) where T : class
        {
            return Task.Run(() => _equipmentParameterRespository.DeleteEquipmentParameter(entity));
        }

        public Task<bool> DeleteEquipmentParameterBy<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return Task.Run(() => _equipmentParameterRespository.DeleteEquipmentParameterBy(whereLambda));
        }

        public Task<bool> DeleteEquipmentCompany(int id)
        {
            return Task.Run(() => _equipmentParameterRespository.DeleteEquipmentCompany(id));
        }

        public Task<bool> DeleteEquipmentCategory(int id)
        {
            return Task.Run(() => _equipmentParameterRespository.DeleteEquipmentCategory(id));
        }

        public Task<bool> DeleteEquipmentManufacturer(int id)
        {
            return Task.Run(() => _equipmentParameterRespository.DeleteEquipmentManufacturer(id));
        }
    }
}
