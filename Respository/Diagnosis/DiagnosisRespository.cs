using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class DiagnosisRespository : IDiagnosisRespository
    {
        private readonly SQLServerDBContext _context;
        public DiagnosisRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<StationLoopDiagnosticData> GetLoopDiagnosticDataByStation(int stationID)
        {
            List<StationLoopDiagnosticData> loopDiagnosticDatas = new List<StationLoopDiagnosticData>();

            loopDiagnosticDatas = (from diagnosis in _context.StationLoopDiagnosticDatas
                                   join loop in _context.StationLoops on diagnosis.ID equals loop.ID
                                   join category in _context.EquipmentCategories on loop.EquipmentCategoryID equals category.ID
                                   join status in _context.DiagnosticStatusDescriptions on diagnosis.LoopStatusID equals status.ID
                                   where loop.StationID == stationID
                                   select new StationLoopDiagnosticData
                                   {
                                       ID = diagnosis.ID,
                                       DateTime = diagnosis.DateTime,
                                       LoopName = loop.AbbrName,
                                       Flowmeter = category.Name,
                                       FMDiagnositcResultID = diagnosis.FMDiagnositcResultID,
                                       TTDiagnositcResultID = diagnosis.TTDiagnositcResultID,
                                       PTDiagnositcResultID = diagnosis.PTDiagnositcResultID,
                                       FCDiagnositcResultID = diagnosis.FCDiagnositcResultID,
                                       VOSDiagnositcResultID = diagnosis.VOSDiagnositcResultID,
                                       LoopStatusID = diagnosis.LoopStatusID,
                                       FMDiagnosticResult = _context.DiagnosticResultDescriptions.FirstOrDefault(x => x.ID == diagnosis.FMDiagnositcResultID).DescriptionCN,
                                       TTDiagnosticResult = _context.DiagnosticResultDescriptions.FirstOrDefault(x => x.ID == diagnosis.TTDiagnositcResultID).DescriptionCN,
                                       PTDiagnosticResult = _context.DiagnosticResultDescriptions.FirstOrDefault(x => x.ID == diagnosis.PTDiagnositcResultID).DescriptionCN,
                                       FCDiagnosticResult = _context.DiagnosticResultDescriptions.FirstOrDefault(x => x.ID == diagnosis.FCDiagnositcResultID).DescriptionCN,
                                       VOSDiagnosticResult = _context.DiagnosticResultDescriptions.FirstOrDefault(x => x.ID == diagnosis.VOSDiagnositcResultID).DescriptionCN,
                                       LoopStatus = status.DescriptionCN
                                   }).ToList();

            return loopDiagnosticDatas;
        }

        public List<StationEquipmentDiagnosticData> GetEquipmentDiagnosticDataByStation(int stationID)
        {
            List<StationEquipmentDiagnosticData> equipmentDiagnosticDatas = new List<StationEquipmentDiagnosticData>();

            equipmentDiagnosticDatas = (from diagnosis in _context.StationEquipmentDiagnosticDatas
                                        join equipment in _context.StationEquipments on diagnosis.ID equals equipment.ID
                                        join category in _context.EquipmentCategories on equipment.EquipmentCategoryID equals category.ID
                                        join result in _context.DiagnosticResultDescriptions on diagnosis.ResultID equals result.ID
                                        where equipment.StationID == stationID
                                        select new StationEquipmentDiagnosticData
                                        {
                                            ID = diagnosis.ID,
                                            DateTime = diagnosis.DateTime,
                                            EquipmentName = equipment.AbbrName,
                                            EquipmentCategory = category.Name,
                                            Manufacturer = equipment.Manufacturer,
                                            Model = equipment.Model,
                                            ResultID = diagnosis.ResultID,
                                            Result = result.DescriptionCN
                                        }).ToList();

            return equipmentDiagnosticDatas;
        }
    }
}
