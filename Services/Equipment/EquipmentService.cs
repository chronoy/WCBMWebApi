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
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRespository _equipmentRespository;
        public EquipmentService(IEquipmentRespository equipmentRespository)
        {
            _equipmentRespository = equipmentRespository;
        }
        public Task<List<T>> GetEquipmentParameters<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return Task.Run(() => _equipmentRespository.GetEquipmentParameters(whereLambda));
        }

        public Task<string> AddEquipmentParameter<T>(T entity) where T : class
        {
            return Task.Run(() => _equipmentRespository.AddEquipmentParameter(entity));
        }

        public Task<string> AddEquipmentParameters<T>(List<T> entities) where T : class
        {
            return Task.Run(() => _equipmentRespository.AddEquipmentParameters(entities));
        }

        public Task<bool> UpdateEquipmentParameter<T>(T entity) where T : class
        {
            return Task.Run(() => _equipmentRespository.UpdateEquipmentParameter(entity));
        }

        public Task<bool> UpdateEquipmentParameters<T>(List<T> listEntity) where T : class
        {
            return Task.Run(() => _equipmentRespository.UpdateEquipmentParameters(listEntity));
        }

        public Task<bool> DeleteEquipmentParameter<T>(T entity) where T : class
        {
            return Task.Run(() => _equipmentRespository.DeleteEquipmentParameter(entity));
        }

        public Task<bool> DeleteEquipmentParameterBy<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return Task.Run(() => _equipmentRespository.DeleteEquipmentParameterBy(whereLambda));
        }
    }
}
