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

        public StationEquipment GetStationEquipmentByID(int ID)
        {
            return (from equipment in _context.StationEquipments
                    join
                    station in _context.Stations
                    on equipment.StationID equals station.ID
                    where equipment.ID == ID
                    select new StationEquipment
                    {
                        ID = equipment.ID,
                        Name = equipment.Name,
                        AbbrName = equipment.AbbrName,
                        CollectDataTypeID = equipment.CollectDataTypeID,
                        EquipmentCategoryID = equipment.EquipmentCategoryID,
                        StationID = equipment.StationID,
                        StationAbbrName = station.AbbrName
                    }).FirstOrDefault();
        }
    }
}
