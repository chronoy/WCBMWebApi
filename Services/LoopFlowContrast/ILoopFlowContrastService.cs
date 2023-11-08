using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILoopFlowContrastService
    {
        public Task<List<LoopFlowContrastConfig>> GetLoopFlowContrastConfigs(int stationID, List<int> contrastStates, DateTime beginDateTime, DateTime endDateTime);
        public Task<string> AddLoopFlowContrastConfig(LoopFlowContrastConfig entity);
        public Task<string> UpdateLoopFlowContrastConfig(LoopFlowContrastConfig entity);
        public Task<bool> DeleteLoopFlowContrastConfig(int configID);
        public Task<string> FinishLoopFlowContrastConfig(int configID);
        public Task<LoopFlowContrastRecord> GetLoopFlowContrastRecord(int configID);
    }
}
