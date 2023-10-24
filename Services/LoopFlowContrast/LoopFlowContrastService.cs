using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoopFlowContrastService: ILoopFlowContrastService
    {
        private readonly ILoopFlowContrastRespository _respository;
        public LoopFlowContrastService(ILoopFlowContrastRespository respository)
        {
            _respository = respository;
        }

        public Task<List<LoopFlowContrastConfig>> GetLoopFlowContrastConfigs(int stationID, List<int> contrastStates, DateTime beginDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetLoopFlowContrastConfigs(stationID, contrastStates, beginDateTime,endDateTime));
        }

        public Task<string> AddLoopFlowContrastConfig(LoopFlowContrastConfig entity)
        {
            return Task.Run(() => _respository.AddLoopFlowContrastConfig(entity));
        }


        public Task<string> UpdateLoopFlowContrastConfig(LoopFlowContrastConfig entity)
        {
            return Task.Run(() => _respository.UpdateLoopFlowContrastConfig(entity));
        }

        public Task<bool> DeleteLoopFlowContrastConfig(int configID)
        {
            return Task.Run(() => _respository.DeleteLoopFlowContrastConfig(configID));
        }
    }
}
