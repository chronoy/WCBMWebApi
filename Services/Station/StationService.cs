using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StationService : IStationService
    {
        private readonly IStationRespository _respository;
        public StationService(IStationRespository respository)
        {
            _respository = respository;
        }

        public Task<List<Station>> GetStations()
        {
            return Task.Run(() => _respository.GetStations());
        }

        public Task<Station> GetStationByID(int ID)
        {
            return Task.Run(() => _respository.GetStationByID(ID));
        }
    }
}
