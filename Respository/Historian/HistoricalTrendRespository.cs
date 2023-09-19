using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class HistoricalTrendRespository : IHistoricalTrendRespository
    {
        private readonly SQLServerDBContext _context;
        public HistoricalTrendRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<Trend> GetHistoricalTrend(string IFIXNodeName, int loopID, int trendGroupID)
        {
            var historicalTrend = (from loop in _context.StationLoops
                                   join station in _context.Stations on loop.StationID equals station.ID
                                   join tg in _context.TrendGroups on loop.CollectDataTypeID equals tg.CollectDataTypeID
                                   join tt in _context.TrendTags on tg.ID equals tt.TrendGroupID
                                   where loop.ID == loopID && tg.ID == trendGroupID
                                   orderby tt.ID
                                   select new Trend
                                   {
                                       Name = $"{station.AbbrName}_{loop.AbbrName}_{tt.Name}",
                                       Address = $"{IFIXNodeName}:{station.AbbrName}_{loop.AbbrName}_{tt.Address}",
                                       HighLimit = tt.HighLimit,
                                       LowLimit = tt.LowLimit,
                                       Description = tt.Description,
                                   }).ToList();
            return historicalTrend;
        }
    }
}
