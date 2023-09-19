using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class StationEquipmentRespository: IStationEquipmentRespository
    {
        private readonly SQLServerDBContext _context;
        public StationEquipmentRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<StationEquipment> GetStationEquipments()
        {
            return _context.StationEquipments.ToList();
        }

        public List<StationEquipment> GetStationEquipmentsBySttaion(int stationID)
        {
            return _context.StationEquipments.Where(obj=>obj.StationID==stationID).ToList();
        }
    }
}
