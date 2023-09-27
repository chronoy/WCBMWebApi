using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IStationEquipmentService
    {
        public Task<List<StationEquipment>> GetStationEquipments();
        public Task<List<StationEquipment>> GetStationEquipmentsByStation(int stationID);
        public Task<StationEquipment> GetStationEquipmentByID(int ID);
    }
}
