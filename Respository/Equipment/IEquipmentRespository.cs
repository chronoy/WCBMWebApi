using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface IEquipmentRespository
    {
        public List<T> GetEquipmentParameters<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        public string AddEquipmentParameter<T>(T entity) where T : class;
        public string AddEquipmentParameters<T>(List<T> entities) where T : class;
        public bool UpdateEquipmentParameter<T>(T entity) where T : class;
        public bool UpdateEquipmentParameters<T>(List<T> listEntity) where T : class;
        public bool DeleteEquipmentParameter<T>(T entity) where T : class;
        public bool DeleteEquipmentParameterBy<T>(Expression<Func<T, bool>> whereLambda) where T : class;
    }
}
