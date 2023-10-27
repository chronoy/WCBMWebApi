using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IKeyParameterService
    {
        public Task<List<KeyParameter>> GetKeyParametersByLoop(List<int> loopIDs);

        public Task<List<KeyParametersChangeRecord>> GetKeyParametersChangeRecordByLoop(DateTime beginTime, DateTime endTime, List<int> loopIDs);
    }
}
