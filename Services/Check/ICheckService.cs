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
        public Task<List<StationEquipmentCheckData>> GetStationEquipmentCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime);
        public Task<List<StationLoopCheckData>> GetStationLoopCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime);
    }
}
