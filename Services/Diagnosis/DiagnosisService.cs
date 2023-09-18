using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DiagnosisService : IDiagnosisService
    {
        private readonly IDiagnosisRespository _diagnosisRespository;
        public DiagnosisService(IDiagnosisRespository diagnosisRespository)
        {
            _diagnosisRespository = diagnosisRespository;
        }

        public Task<List<StationLoopDiagnosticData>> GetLoopDiagnosticDataByStation(int stationID)
        {
            return Task.Run(() => _diagnosisRespository.GetLoopDiagnosticDataByStation(stationID));
        }

        public Task<List<StationEquipmentDiagnosticData>> GetEquipmentDiagnosticDataByStation(int stationID)
        {
            return Task.Run(() => _diagnosisRespository.GetEquipmentDiagnosticDataByStation(stationID));
        }

        public Task<List<DiagnosticDataDetail>> GetLoopDiagnosticDataDetailByLoop(int loopID, string manufacturer, string diagnosticType)
        {
            return Task.Run(() => _diagnosisRespository.GetLoopDiagnosticDataDetailByLoop(loopID, manufacturer, diagnosticType));
        }
    }
}
