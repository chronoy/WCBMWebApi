using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public interface IStationService
    {
        public Task<List<Station>> GetStations();
        public Task<Station> GetStationByID(int ID);
    }
}
