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
                                   orderby trendTag.DeviceID
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

        public List<TrendTag> GetTrendTags(List<int> deviceIds, List<string> deviceTypes)
        {
            var result = new List<TrendTag>();
            var trendTags = _context.TrendTags.ToList();
            foreach ((int id, int i) in deviceIds.Select((id, i) => (id, i)))
            {
                var type = deviceTypes.Count >= i + 1 ? deviceTypes[i].ToLower() : "";
                result.AddRange(trendTags.Where(x => x.DeviceID == id && x.DeviceType.ToLower() == type));
            }
            return result.OrderBy(o => Array.IndexOf(new[] { "Loop", "Equipment" }, o.DeviceType)).ThenBy(t => t.DeviceID).ToList();
        }
    }
}
