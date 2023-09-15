using Models;

namespace Services
{
    public interface IDiagnosisService
    {
        public Task<List<StationLoopDiagnosticData>> GetLoopDiagnosticDataByStation(int stationID);
        public Task<List<StationEquipmentDiagnosticData>> GetEquipmentDiagnosticDataByStation(int stationID);
    }
}