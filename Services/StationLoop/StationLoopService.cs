using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StationLoopService:IStationLoopService
    {
        private readonly IStationLoopRespository _respository;
        public StationLoopService(IStationLoopRespository respository)
        {
            _respository = respository;
        }

        public Task<List<StationLoop>> GetStationLoops()
        {
            return Task.Run(() => _respository.GetStationLoops());
        }

        public Task<List<StationLoop>> GetStationLoopsByStation(int stationID)
        {
            return Task.Run(() => _respository.GetStationLoopsByStation(stationID));
        }
    }

}
