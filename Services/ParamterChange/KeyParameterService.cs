using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class KeyParameterService: IKeyParameterService
    {
        public readonly IKeyParameterRespository _respository;
        public KeyParameterService(IKeyParameterRespository respository)
        {
            _respository = respository;
        }

        public Task<List<KeyParameter>> GetKeyParametersByLoop(List<int> loopIDs)
        {
            return Task.Run(() => _respository.GetKeyParametersByLoop(loopIDs));
        }

        public Task<List<KeyParametersChangeRecord>> GetKeyParametersChangeRecordByLoop(DateTime beginTime, DateTime endTime, List<int> loopIDs)
        {
            return Task.Run(() => _respository.GetKeyParametersChangeRecordByLoop(beginTime, endTime, loopIDs));
        }
    }
}
