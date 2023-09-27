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

        public List<Trend> GetHistoricalTrend(string IFIXNodeName, List<string> trendTags)
        {
            var historicalTrend = (from trendTag in _context.TrendTags
                                   where trendTags.Contains(trendTag.Address)
                                   orderby trendTag.ID
                                   select new Trend
                                   {
                                       Name = trendTag.Name.Replace("", "_"),
                                       Address = $"{IFIXNodeName}:{trendTag.Address}",
                                       HighLimit = trendTag.HighLimit,
                                       LowLimit = trendTag.LowLimit,
                                       Description = trendTag.Description,
                                   }).ToList();
            return historicalTrend;
        }

        public List<TrendTag> GetTrendTags()
        {
            return _context.TrendTags.ToList();
        }
    }
}
