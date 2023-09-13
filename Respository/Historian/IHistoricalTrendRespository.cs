using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface IHistoricalTrendRespository
    {
        public List<Trend> GetHistoricalTrend(string IFIXNodeName, int loopID, int trendGroupID);
    }
}
