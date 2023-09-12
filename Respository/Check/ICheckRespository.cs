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
        public List<StationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime);
        public List<StationLoopCheckData> GetStationLoopCheckReport(string reportCategory, string brandName, int loopID, DateTime startDateTime, DateTime endDateTime);
    }
}
