using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IHistoricalTrendService
    {
        public Task<Dictionary<string, object>> GetHistoricalTrendsData(int LoopID,
                                             int TrendGroupID,
                                             DateTime startDateTime,
                                             string interval,
                                             string duration);

        public Task<List<Dictionary<string, object>>> GetExportHistoricalTrendsData(int LoopID,
                                            int TrendGroupID,
                                            DateTime startDateTime,
                                            string interval,
                                            string duration);

        public Task<byte[]> ExportHistoricalTrendsDataReport(int LoopID,
                                            int TrendGroupID,
                                            DateTime startDateTime,
                                            string interval,
                                            string duration,
                                            string templatePath);
    }
}
