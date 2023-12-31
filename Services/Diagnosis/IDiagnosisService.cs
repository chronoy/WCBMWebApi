﻿using Models;

namespace Services
{
    public interface IDiagnosisService
    {
        public Task<List<StationLoopDiagnosticData>> GetLoopDiagnosticDataByStation(int stationID);
        public Task<List<StationEquipmentDiagnosticData>> GetEquipmentDiagnosticDataByStation(int stationID);

        public Task<List<DiagnosticDataDetail>> GetLoopDiagnosticDataDetailByLoop(int loopID, string manufacturer, string diagnosisType);

        public Task<List<DiagnosticDataDetail>> GetEquipmentDiagnosticDataDetailByEquipment(int equipmentID, string manufacturer);

        public Task<List<StationLoopDiagnosticData>> GetLoopStatusByStations(List<int> stationIDs);
    }
}