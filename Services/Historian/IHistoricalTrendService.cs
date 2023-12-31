﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IHistoricalTrendService
    {
        public Task<Dictionary<string, object>> GetHistoricalTrendsData(List<string> trendTags,
                                             DateTime startDateTime,
                                             string interval,
                                             string duration);

        public Task<List<Dictionary<string, object>>> GetExportHistoricalTrendsData(List<string> trendTags,
                                            DateTime startDateTime,
                                            string interval,
                                            string duration);

        public Task<byte[]> ExportHistoricalTrendsDataReport(List<string> trendTags,
                                            DateTime startDateTime,
                                            string interval,
                                            string duration,
                                            string templatePath);

        public List<Dictionary<string, object>> GetHistoricalData(
                                   DateTime startDateTime,
                                   string interval,
                                   string duration,
                                   List<string> tagsAddress
                                   );

        public Task<List<TrendTag>> GetTrendTags(List<int> deviceIds, List<string> deviceTypes);
    }
}
