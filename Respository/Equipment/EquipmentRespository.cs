using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class EquipmentRespository: IEquipmentRespository
    {
        private readonly SQLServerDBContext _context;
        public EquipmentRespository(SQLServerDBContext context)
        {
            _context = context;
        }
    }
}
