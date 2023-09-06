using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class CheckRespository : ICheckRespository
    {
        private readonly SQLServerDBContext _context;
        public CheckRespository(SQLServerDBContext context)
        {
            _context = context;
        }
        public IEnumerable<StationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime)
        {
            //IEnumerable<StationEquipmentCheckData> abb=_context.ABBGCCheckDatas;
            //IEnumerable<StationEquipmentCheckData> daniel = _context.DanielGCCheckDatas;
            List<User> u = _context.Users.ToList();
            return null;
        }
    }
}
