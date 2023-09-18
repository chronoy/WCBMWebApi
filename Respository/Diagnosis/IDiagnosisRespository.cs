using Models;

namespace Respository
{
    public interface IDiagnosisRespository
    {
        public List<StationLoopDiagnosticData> GetLoopDiagnosticDataByStation(int stationID);
        public List<StationEquipmentDiagnosticData> GetEquipmentDiagnosticDataByStation(int stationID);

        public List<DiagnosticDataDetail> GetLoopDiagnosticDataDetailByLoop(int loopID,string manufacturer, string diagnosticType);
    }
}