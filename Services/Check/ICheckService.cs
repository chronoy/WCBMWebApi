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
        public Task<List<HistoricalStationEquipmentCheckData>> GetStationEquipmentCheckReport(string reportCategory, string manufacturer, int equipmentID, DateTime startDateTime, DateTime endDateTime);
        public Task<List<HistoricalStationLoopCheckData>> GetStationLoopCheckReport(string reportCategory, string manufacturer, int loopID, DateTime startDateTime, DateTime endDateTime);
        public Task<Dictionary<string, object>> GetManualCheckData(int loopID, string manufacturer);
        public Task<Dictionary<string, object>> GetOfflineCheck(OfflineCheck offlineCheck);

        public Task<List<GCRepeatabilityCheckData>> GetOnlineGCRepeatabilityCheck(int ID, List<Data> firstDatas, List<Data> secondDatas);
        public Task<List<UnnormalizedComponentsCheckData>> GetGCUnnormalizedComponentsCheck(int equipmentID,
                                                                                     DateTime startDateTime,
                                                                                     string interval,
                                                                                     string duration);
    }
}
