using Models;

namespace Respository
{
    public interface IDiagnosisRespository
    {
        public List<StationLoopDiagnosticData> GetLoopDiagnosticDataByStation(int stationID);
        public List<StationEquipmentDiagnosticData> GetEquipmentDiagnosticDataByStation(int stationID);

        public List<DiagnosticDataDetail> GetLoopDiagnosticDataDetailByLoop(int loopID,string manufacturer, string diagnosisType);
        public List<DiagnosticDataDetail> GetEquipmentDiagnosticDataDetailByEquipment(int equipmentID, string manufacturer);

        public List<StationLoopDiagnosticData> GetLoopStatusByStations(List<int> stationIDs);
    }
}