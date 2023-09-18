using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EquipmentService: IEquipmentService
    {
        private readonly IEquipmentRespository _equipmentRespository;
        public EquipmentService(IEquipmentRespository equipmentRespository)
        {
            _equipmentRespository = equipmentRespository;
        }
    }
}
