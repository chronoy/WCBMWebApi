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

        public Task<List<StationEquipment>> GetStationEquipmentsBySttaion(int stationID)
        {
            return Task.Run(() => _respository.GetStationEquipmentsBySttaion(stationID));
        }
    }
}
