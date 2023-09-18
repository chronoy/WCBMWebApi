using System.Linq.Expressions;

namespace Services
{
    public interface IEquipmentService
    {
        public Task<List<T>> GetEquipmentParameters<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        public Task<string> AddEquipmentParameter<T>(T entity) where T : class;
        public Task<string> AddEquipmentParameters<T>(List<T> entities) where T : class;
        public Task<bool> UpdateEquipmentParameter<T>(T entity) where T : class;
        public Task<bool> UpdateEquipmentParameters<T>(List<T> listEntity) where T : class;
        public Task<bool> DeleteEquipmentParameter<T>(T entity) where T : class;
        public Task<bool> DeleteEquipmentParameterBy<T>(Expression<Func<T, bool>> whereLambda) where T : class;
    }
}