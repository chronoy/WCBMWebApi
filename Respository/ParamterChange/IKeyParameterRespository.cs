using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface IKeyParameterRespository
    {
        public List<KeyParameter> GetKeyParametersByLoop(List<int> loopIDs);
        public List<KeyParametersChangeRecord> GetKeyParametersChangeRecordByLoop(DateTime beginTime, DateTime endTime, List<int> loopIDs);
    }
}
