using Microsoft.Extensions.Configuration;
using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StationEquipmentService: IStationEquipmentService
    {
        private readonly IStationEquipmentRespository _respository;
        public StationEquipmentService(IStationEquipmentRespository respository)
        {
            _respository = respository;
        }

        public Task<List<StationEquipment>> GetStationEquipments()
        {
            return Task.Run(() => _respository.GetStationEquipments());
        }

        public Task<StationEquipment> GetStationEquipmentByID(int ID)
        {
            return Task.Run(() => _respository.GetStationEquipmentByID(ID));
        }

        public Task<List<StationEquipment>> GetStationEquipmentsByStation(int stationID)
        {
            return Task.Run(() => _respository.GetStationEquipmentsBySttaion(stationID));
        }
    }
}
