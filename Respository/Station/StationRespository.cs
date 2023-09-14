using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class StationRespository:IStationRespository
    {
        private readonly SQLServerDBContext _context;
        public StationRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public Station GetStationByID(int ID)
        {
            return _context.Stations.FirstOrDefault(obj=>obj.ID==ID);
        }

        public List<Station> GetStations()
        {
            return _context.Stations.ToList();
        }
    }
}
