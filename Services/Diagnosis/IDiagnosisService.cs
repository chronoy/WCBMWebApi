using Models;

namespace Services
{
    public interface IDiagnosisService
    {
        public Task<List<StationLoopDiagnosticData>> GetLoopDiagnosticDataByStation(int stationID);
        public Task<List<StationEquipmentDiagnosticData>> GetEquipmentDiagnosticDataByStation(int stationID);

        public Task<List<DiagnosticDataDetail>> GetLoopDiagnosticDataDetailByLoop(int loopID, string manufacturer, string diagnosticType);
    }
}