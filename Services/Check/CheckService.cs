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

        public Task<List<StationEquipmentCheckData>> GetStationEquipmentCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetStationEquipmentCheckReport(reportCategory, brandName, equipmentID, startDateTime, endDateTime));
        }

        public Task<List<StationLoopCheckData>> GetStationLoopCheckReport(string reportCategory, string brandName, int loopID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetStationLoopCheckReport(reportCategory, brandName, loopID, startDateTime, endDateTime));
        }
    }
}
