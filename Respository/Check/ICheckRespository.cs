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
        public IEnumerable<StationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime);
    }
}
