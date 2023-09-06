using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Respository;

namespace Services
{ 
    public class CheckService:ICheckService
    {
        private readonly ICheckRespository _respository;
        public CheckService(IConfiguration configuration, ICheckRespository checkRespository)
        {
            _respository = checkRespository;
        }

        public Task<IEnumerable<StationEquipmentCheckData>> GetStationEquipmentCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime)
        {
            return Task.Run(() => _respository.GetStationEquipmentCheckReport(reportCategory, equipmentID, startDateTime, endDateTime));
        }
    }
}
