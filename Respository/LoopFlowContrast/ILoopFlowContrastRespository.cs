using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface ILoopFlowContrastRespository
    {

        public List<LoopFlowContrastConfig> GetLoopFlowContrastConfigs(int stationID, List<int> contrastStates, DateTime beginDateTime, DateTime endDateTime);
        public string AddLoopFlowContrastConfig(LoopFlowContrastConfig entity);
        public string UpdateLoopFlowContrastConfig(LoopFlowContrastConfig entity);
        public bool DeleteLoopFlowContrastConfig(int configID);
        public LoopFlowContrastConfig GetLoopFlowContrastConfig(int configID);
        public string FinishLoopFlowContrastConfig(LoopFlowContrastConfig config, LoopFlowContrastRecord record);
        public List<LoopFlowContrastTag> GetLoopFlowContrastTags(int LoopID, string IFIXNodeName);

        public LoopFlowContrastRecord GetLoopFlowContrastRecord(int configID);
    }
}
