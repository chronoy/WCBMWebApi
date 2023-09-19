using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICheckService
    {
        public Task<List<HistoricalStationEquipmentCheckData>> GetStationEquipmentCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime);
        public Task<List<HistoricalStationLoopCheckData>> GetStationLoopCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime);
        public Task<Dictionary<string, object>> GetManualCheckData(int loopID, string brandName);

        public Task<Dictionary<string, object>> GetOfflineCheck(OfflineCheck offlineCheck);
    }
}
