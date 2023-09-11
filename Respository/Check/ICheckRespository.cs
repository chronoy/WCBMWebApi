using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface ICheckRespository
    {
        public List<StationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime);
        public List<StationLoopCheckData> GetStationLoopCheckReport(string reportCategory, int loopID, string startDateTime, string endDateTime);
    }
}
