using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class StationRespository : IStationRespository
    {
        private readonly SQLServerDBContext _context;
        public StationRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public Station GetStationByID(int ID)
        {
            return _context.Stations.FirstOrDefault(obj => obj.ID == ID);
        }

        public List<Station> GetStations()
        {
            return _context.Stations.ToList();
        }

        public List<Station> GetStationsByStations(List<int> stationIDs)
        {
            return (from station in _context.Stations.Where(station => stationIDs.Contains(station.ID))
                    select new Station
                    {
                        ID = station.ID,
                        Name = station.Name,
                        AbbrName = station.AbbrName,
                        Phone = station.Phone,
                        Address = station.Address,
                        PostalCode = station.PostalCode,
                        Description = station.Description,
                        AreaID = station.AreaID,
                        CollectorID = station.CollectorID,
                        Loops =_context.StationLoops.Where(loop=>loop.StationID==station.ID).ToList(),
                    }).ToList();
        }
    }
}
