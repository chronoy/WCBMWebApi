using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                                       LoopCategory = category.Name,
                                       LoopDescription = category.Description,
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
                                       LoopStatus = status.DescriptionCN,
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

        public List<DiagnosticDataDetail> GetLoopDiagnosticDataDetailByLoop(int loopID, string manufacturer, string diagnosisType)
        {
            List<DiagnosticDataDetail> details=new List<DiagnosticDataDetail>();
            switch(manufacturer)
            {
                case "Daniel":
                    switch(diagnosisType)
                    {
                        case "TT":
                            using (DanielTTDiagnosticDataDetail detail = _context.DanielTTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "温度值(+/-2DegC)", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "温度报警", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "温度状态", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = detail.v2.ToString("F4") });
                            }
                            break;
                        case "PT":
                            using (DanielPTDiagnosticDataDetail detail = _context.DanielPTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "压力值(+/-100kPa)", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力报警", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力状态", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = detail.v2.ToString("F4") });
                            }
                            break;
                        case "FM":
                            using (DanielFMDiagnosticDataDetail detail = _context.DanielFMDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                int[] NotAvailable = new int[] { 1, 17, 20, 21, 22, 33, 36, 255};
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = NotAvailable.Contains(detail.P0)  ?"N/A":detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "报警(= 0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = NotAvailable.Contains(detail.P1) ? "N/A" : detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "温度(-20~100Deg.C)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = NotAvailable.Contains(detail.P2) ? "N/A" : detail.v2.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力(>1000KPa)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = NotAvailable.Contains(detail.P3) ? "N/A" : detail.v3.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A1信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = NotAvailable.Contains(detail.P4) ? "N/A" : detail.v4.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A2信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P5).DescriptionCN, Value = NotAvailable.Contains(detail.P5) ? "N/A" : detail.v5.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B1信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P6).DescriptionCN, Value = NotAvailable.Contains(detail.P6) ? "N/A" : detail.v6.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B2信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = NotAvailable.Contains(detail.P7) ? "N/A" : detail.v7.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C1信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = NotAvailable.Contains(detail.P8) ? "N/A" : detail.v8.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C2信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = NotAvailable.Contains(detail.P9) ? "N/A" : detail.v9.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D1信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = NotAvailable.Contains(detail.P10) ? "N/A" : detail.v10.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D2信号接受率(>=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = NotAvailable.Contains(detail.P11) ? "N/A" : detail.v11.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A1信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P12).DescriptionCN, Value = NotAvailable.Contains(detail.P12) ? "N/A" : detail.v12.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A2信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P13).DescriptionCN, Value = NotAvailable.Contains(detail.P13) ? "N/A" : detail.v13.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B1信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = NotAvailable.Contains(detail.P14) ? "N/A" : detail.v14.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B2信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = NotAvailable.Contains(detail.P15) ? "N/A" : detail.v15.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C1信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = NotAvailable.Contains(detail.P16) ? "N/A" : detail.v16.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C2信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P17).DescriptionCN, Value = NotAvailable.Contains(detail.P17) ? "N/A" : detail.v17.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D1信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P18).DescriptionCN, Value = NotAvailable.Contains(detail.P18) ? "N/A" : detail.v18.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D2信号增益(<64)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P19).DescriptionCN, Value = NotAvailable.Contains(detail.P19) ? "N/A" : detail.v19.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A1信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P20).DescriptionCN, Value = NotAvailable.Contains(detail.P20) ? "N/A" : detail.v20.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A2信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P21).DescriptionCN, Value = NotAvailable.Contains(detail.P21) ? "N/A" : detail.v21.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B1信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P22).DescriptionCN, Value = NotAvailable.Contains(detail.P22) ? "N/A" : detail.v22.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B2信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P23).DescriptionCN, Value = NotAvailable.Contains(detail.P23) ? "N/A" : detail.v23.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C1信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P24).DescriptionCN, Value = NotAvailable.Contains(detail.P24) ? "N/A" : detail.v24.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C2信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P25).DescriptionCN, Value = NotAvailable.Contains(detail.P25) ? "N/A" : detail.v25.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D1信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P26).DescriptionCN, Value = NotAvailable.Contains(detail.P26) ? "N/A" : detail.v26.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D2信噪比(>21)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P27).DescriptionCN, Value = NotAvailable.Contains(detail.P27) ? "N/A" : detail.v27.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A声速偏差率(<0.20 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P28).DescriptionCN, Value = NotAvailable.Contains(detail.P28) ? "N/A" : detail.v28.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B声速偏差率(<0.20 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P29).DescriptionCN, Value = NotAvailable.Contains(detail.P29) ? "N/A" : detail.v29.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C声速偏差率(<0.20 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P30).DescriptionCN, Value = NotAvailable.Contains(detail.P30) ? "N/A" : detail.v30.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D声速偏差率(<0.20 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P31).DescriptionCN, Value = NotAvailable.Contains(detail.P31) ? "N/A" : detail.v31.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机计算声速偏差率(<0.20 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P32).DescriptionCN, Value = NotAvailable.Contains(detail.P32) ? "N/A" : detail.v32.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "剖面系数", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P33).DescriptionCN, Value = NotAvailable.Contains(detail.P33) ? "N/A" : detail.v33.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流速对称性(0.95-1.05)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P34).DescriptionCN, Value = NotAvailable.Contains(detail.P34) ? "N/A" : detail.v34.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流速交叉流(0.95-1.05)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P35).DescriptionCN, Value = NotAvailable.Contains(detail.P35) ? "N/A" : detail.v35.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道A脉动流(<5.5 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P36).DescriptionCN, Value = NotAvailable.Contains(detail.P36) ? "N/A" : detail.v36.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道B脉动流(<2.5 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P37).DescriptionCN, Value = NotAvailable.Contains(detail.P37) ? "N/A" : detail.v37.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道C脉动流(<2.5 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P38).DescriptionCN, Value = NotAvailable.Contains(detail.P38) ? "N/A" : detail.v38.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道D脉动流(<5.5 %)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P39).DescriptionCN, Value = NotAvailable.Contains(detail.P39) ? "N/A" : detail.v39.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "气体漩涡角(+/-1.99°)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P40).DescriptionCN, Value = NotAvailable.Contains(detail.P40) ? "N/A" : detail.v40.ToString("F4") });
                            }
                            break;
                        case "FC":
                            using (DanielFCDiagnosticDataDetail detail = _context.DanielFCDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机过程", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机系统", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = detail.v2.ToString("F4") });
                            }
                            break;
                        case "VOS":
                            using (DanielVOSDiagnosticDataDetail detail = _context.DanielVOSDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "FC声速偏差(<0.20 %)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "FM声速偏差(<0.20 %)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                    }
                    break;
                case "Elster":
                    switch (diagnosisType)
                    {
                        case "TT":
                            using (ElsterTTDiagnosticDataDetail detail = _context.ElsterTTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "温度状态", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "PT":
                            using (ElsterPTDiagnosticDataDetail detail = _context.ElsterPTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力状态", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "FM":
                            using (ElsterFMDiagnosticDataDetail detail = _context.ElsterFMDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                int[] NotAvailable = new int[] { 1, 17, 20, 21, 22, 33, 36, 255 };
                                switch (_context.StationLoops.FirstOrDefault(obj => obj.ID == loopID).CollectDataTypeID)
                                {
                                    case 2:
                                    case 10:
                                        details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = NotAvailable.Contains(detail.P0) ? "N/A" : detail.v1.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道1报警", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = NotAvailable.Contains(detail.P1) ? "N/A" : detail.v2.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道2报警", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = NotAvailable.Contains(detail.P2) ? "N/A" : detail.v3.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道3报警", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = NotAvailable.Contains(detail.P3) ? "N/A" : detail.v4.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道4报警", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = NotAvailable.Contains(detail.P4) ? "N/A" : detail.v5.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道1接受率(=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = NotAvailable.Contains(detail.P8) ? "N/A" : detail.v9.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道2接受率(=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = NotAvailable.Contains(detail.P9) ? "N/A" : detail.v10.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道3接受率(=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = NotAvailable.Contains(detail.P10) ? "N/A" : detail.v11.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道4接受率(=100)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = NotAvailable.Contains(detail.P11) ? "N/A" : detail.v12.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A1信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = NotAvailable.Contains(detail.P14) ? "N/A" : detail.v15.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B1信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = NotAvailable.Contains(detail.P15) ? "N/A" : detail.v16.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A2信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = NotAvailable.Contains(detail.P16) ? "N/A" : detail.v17.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B2信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P17).DescriptionCN, Value = NotAvailable.Contains(detail.P17) ? "N/A" : detail.v18.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A3信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P18).DescriptionCN, Value = NotAvailable.Contains(detail.P18) ? "N/A" : detail.v19.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B3信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P19).DescriptionCN, Value = NotAvailable.Contains(detail.P19) ? "N/A" : detail.v20.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A4信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P20).DescriptionCN, Value = NotAvailable.Contains(detail.P20) ? "N/A" : detail.v21.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B4信号增益(<50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P21).DescriptionCN, Value = NotAvailable.Contains(detail.P21) ? "N/A" : detail.v22.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道1声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P38).DescriptionCN, Value = NotAvailable.Contains(detail.P38) ? "N/A" : detail.v39.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道2声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P39).DescriptionCN, Value = NotAvailable.Contains(detail.P39) ? "N/A" : detail.v40.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道3声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P40).DescriptionCN, Value = NotAvailable.Contains(detail.P40) ? "N/A" : detail.v41.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道4声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P41).DescriptionCN, Value = NotAvailable.Contains(detail.P41) ? "N/A" : detail.v42.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "流量计算机计算声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P44).DescriptionCN, Value = NotAvailable.Contains(detail.P44) ? "N/A" : detail.v45.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "剖面系数(1.0467~1.0695)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P45).DescriptionCN, Value = NotAvailable.Contains(detail.P45) ? "N/A" : detail.v46.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "气体漩涡角(-0.72°~1.28°)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P49).DescriptionCN, Value = NotAvailable.Contains(detail.P49) ? "N/A" : detail.v50.ToString("F4") });
                                        break;
                                    case 7:
                                    case 12:
                                        details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = NotAvailable.Contains(detail.P0) ? "N/A" : detail.v1.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道报警", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = NotAvailable.Contains(detail.P7) ? "N/A" : detail.v8.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道1接受率(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = NotAvailable.Contains(detail.P8) ? "N/A" : detail.v9.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道2接受率(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = NotAvailable.Contains(detail.P9) ? "N/A" : detail.v10.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道3接受率(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = NotAvailable.Contains(detail.P10) ? "N/A" : detail.v11.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道4接受率(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = NotAvailable.Contains(detail.P11) ? "N/A" : detail.v12.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道5接受率(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P12).DescriptionCN, Value = NotAvailable.Contains(detail.P12) ? "N/A" : detail.v13.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道6接受率(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P13).DescriptionCN, Value = NotAvailable.Contains(detail.P13) ? "N/A" : detail.v14.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A1信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = NotAvailable.Contains(detail.P14) ? "N/A" : detail.v15.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B1信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = NotAvailable.Contains(detail.P15) ? "N/A" : detail.v16.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A2信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = NotAvailable.Contains(detail.P16) ? "N/A" : detail.v17.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B2信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P17).DescriptionCN, Value = NotAvailable.Contains(detail.P17) ? "N/A" : detail.v18.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A3信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P18).DescriptionCN, Value = NotAvailable.Contains(detail.P18) ? "N/A" : detail.v19.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B3信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P19).DescriptionCN, Value = NotAvailable.Contains(detail.P19) ? "N/A" : detail.v20.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A4信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P20).DescriptionCN, Value = NotAvailable.Contains(detail.P20) ? "N/A" : detail.v21.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B4信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P21).DescriptionCN, Value = NotAvailable.Contains(detail.P21) ? "N/A" : detail.v22.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A5信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P22).DescriptionCN, Value = NotAvailable.Contains(detail.P22) ? "N/A" : detail.v23.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B5信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P23).DescriptionCN, Value = NotAvailable.Contains(detail.P23) ? "N/A" : detail.v24.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A6信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P24).DescriptionCN, Value = NotAvailable.Contains(detail.P24) ? "N/A" : detail.v25.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B6信号增益(<80)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P25).DescriptionCN, Value = NotAvailable.Contains(detail.P25) ? "N/A" : detail.v26.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A1信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P26).DescriptionCN, Value = NotAvailable.Contains(detail.P26) ? "N/A" : detail.v27.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B1信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P27).DescriptionCN, Value = NotAvailable.Contains(detail.P27) ? "N/A" : detail.v28.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A2信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P28).DescriptionCN, Value = NotAvailable.Contains(detail.P28) ? "N/A" : detail.v29.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B2信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P29).DescriptionCN, Value = NotAvailable.Contains(detail.P29) ? "N/A" : detail.v30.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A3信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P30).DescriptionCN, Value = NotAvailable.Contains(detail.P30) ? "N/A" : detail.v31.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B3信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P31).DescriptionCN, Value = NotAvailable.Contains(detail.P31) ? "N/A" : detail.v32.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A4信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P32).DescriptionCN, Value = NotAvailable.Contains(detail.P32) ? "N/A" : detail.v33.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B4信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P33).DescriptionCN, Value = NotAvailable.Contains(detail.P33) ? "N/A" : detail.v34.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A5信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P34).DescriptionCN, Value = NotAvailable.Contains(detail.P34) ? "N/A" : detail.v35.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B5信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P35).DescriptionCN, Value = NotAvailable.Contains(detail.P35) ? "N/A" : detail.v36.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道A6信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P36).DescriptionCN, Value = NotAvailable.Contains(detail.P36) ? "N/A" : detail.v37.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "声道B6信噪比(>2)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P37).DescriptionCN, Value = NotAvailable.Contains(detail.P37) ? "N/A" : detail.v38.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道1声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P38).DescriptionCN, Value = NotAvailable.Contains(detail.P38) ? "N/A" : detail.v39.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道2声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P39).DescriptionCN, Value = NotAvailable.Contains(detail.P39) ? "N/A" : detail.v40.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道3声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P40).DescriptionCN, Value = NotAvailable.Contains(detail.P40) ? "N/A" : detail.v41.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道4声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P41).DescriptionCN, Value = NotAvailable.Contains(detail.P41) ? "N/A" : detail.v42.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道5声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P42).DescriptionCN, Value = NotAvailable.Contains(detail.P42) ? "N/A" : detail.v43.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "通道6声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P43).DescriptionCN, Value = NotAvailable.Contains(detail.P43) ? "N/A" : detail.v44.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "流量计算机计算声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P44).DescriptionCN, Value = NotAvailable.Contains(detail.P44) ? "N/A" : detail.v45.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "剖面系数Axial(1.0175~1.0725)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P45).DescriptionCN, Value = NotAvailable.Contains(detail.P45) ? "N/A" : detail.v46.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "剖面系数Swirl(0.9294~0.9844)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P46).DescriptionCN, Value = NotAvailable.Contains(detail.P46) ? "N/A" : detail.v47.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "对称性Axial(0.9725~1.0275)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P47).DescriptionCN, Value = NotAvailable.Contains(detail.P47) ? "N/A" : detail.v48.ToString("F4") });
                                        details.Add(new DiagnosticDataDetail() { Name = "对称性Swirl(0.9725~1.0275)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P48).DescriptionCN, Value = NotAvailable.Contains(detail.P48) ? "N/A" : detail.v49.ToString("F4") });
                                        break;
                                }
                              }
                            break;
                        case "FC":
                            using (ElsterFCDiagnosticDataDetail detail = _context.ElsterFCDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "与FM通讯状态", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "与GC通讯状态", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "VOS":
                            using (ElsterVOSDiagnosticDataDetail detail = _context.ElsterVOSDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "FC声速偏差(<0.20%)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "FM声速偏差(<0.20%)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                    }
                    break;
                case "Sick":
                    switch (diagnosisType)
                    {
                        case "TT":
                            using (SickTTDiagnosticDataDetail detail = _context.SickTTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "温度变送器通讯", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "温度超限报警", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "PT":
                            using (SickPTDiagnosticDataDetail detail = _context.SickPTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "压力变送器通讯", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力超限报警", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "FM":
                            using (SickFMDiagnosticDataDetail detail = _context.SickFMDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                int[] NotAvailable = new int[] { 1, 17, 20, 21, 22, 33, 36, 255 };
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = NotAvailable.Contains(detail.P0) ? "N/A" : detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "系统状态", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = NotAvailable.Contains(detail.P1) ? "N/A" : detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1报警(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = NotAvailable.Contains(detail.P2) ? "N/A" : detail.v2.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2报警(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = NotAvailable.Contains(detail.P3) ? "N/A" : detail.v3.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3报警(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = NotAvailable.Contains(detail.P4) ? "N/A" : detail.v4.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4报警(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P5).DescriptionCN, Value = NotAvailable.Contains(detail.P5) ? "N/A" : detail.v5.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1信号接收率(>75)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P6).DescriptionCN, Value = NotAvailable.Contains(detail.P6) ? "N/A" : detail.v6.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2信号接收率(>75)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = NotAvailable.Contains(detail.P7) ? "N/A" : detail.v7.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3信号接收率(>75)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = NotAvailable.Contains(detail.P8) ? "N/A" : detail.v8.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4信号接收率(>75)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = NotAvailable.Contains(detail.P9) ? "N/A" : detail.v9.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1AB信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = NotAvailable.Contains(detail.P10) ? "N/A" : detail.v10.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1BA信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = NotAvailable.Contains(detail.P11) ? "N/A" : detail.v11.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2AB信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P12).DescriptionCN, Value = NotAvailable.Contains(detail.P12) ? "N/A" : detail.v12.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2BA信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P13).DescriptionCN, Value = NotAvailable.Contains(detail.P13) ? "N/A" : detail.v13.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3AB信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = NotAvailable.Contains(detail.P14) ? "N/A" : detail.v14.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3BA信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = NotAvailable.Contains(detail.P15) ? "N/A" : detail.v15.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4AB信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = NotAvailable.Contains(detail.P16) ? "N/A" : detail.v16.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4BA信号增益(<93)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P17).DescriptionCN, Value = NotAvailable.Contains(detail.P17) ? "N/A" : detail.v17.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1AB信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P18).DescriptionCN, Value = NotAvailable.Contains(detail.P18) ? "N/A" : detail.v18.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1BA信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P19).DescriptionCN, Value = NotAvailable.Contains(detail.P19) ? "N/A" : detail.v19.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2AB信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P20).DescriptionCN, Value = NotAvailable.Contains(detail.P20) ? "N/A" : detail.v20.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2BA信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P21).DescriptionCN, Value = NotAvailable.Contains(detail.P21) ? "N/A" : detail.v21.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3AB信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P22).DescriptionCN, Value = NotAvailable.Contains(detail.P22) ? "N/A" : detail.v22.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3BA信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P23).DescriptionCN, Value = NotAvailable.Contains(detail.P23) ? "N/A" : detail.v23.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4AB信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P24).DescriptionCN, Value = NotAvailable.Contains(detail.P24) ? "N/A" : detail.v24.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4BA信噪比(>15)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P25).DescriptionCN, Value = NotAvailable.Contains(detail.P25) ? "N/A" : detail.v25.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1声速偏差率(<0.20%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P26).DescriptionCN, Value = NotAvailable.Contains(detail.P26) ? "N/A" : detail.v26.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2声速偏差率(<0.20%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P27).DescriptionCN, Value = NotAvailable.Contains(detail.P27) ? "N/A" : detail.v27.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3声速偏差率(<0.20%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P28).DescriptionCN, Value = NotAvailable.Contains(detail.P28) ? "N/A" : detail.v28.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4声速偏差率(<0.20%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P29).DescriptionCN, Value = NotAvailable.Contains(detail.P29) ? "N/A" : detail.v29.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机计算声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P30).DescriptionCN, Value = NotAvailable.Contains(detail.P30) ? "N/A" : detail.v30.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "剖面系数(1.12-1.22)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P31).DescriptionCN, Value = NotAvailable.Contains(detail.P31) ? "N/A" : detail.v31.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "对称性(0.95-1.05)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P32).DescriptionCN, Value = NotAvailable.Contains(detail.P32) ? "N/A" : detail.v32.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "横向流(0.95-1.05)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P33).DescriptionCN, Value = NotAvailable.Contains(detail.P33) ? "N/A" : detail.v33.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "气体漩涡角(+/-4度)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P34).DescriptionCN, Value = NotAvailable.Contains(detail.P34) ? "N/A" : detail.v34.ToString("F4") });
                            }
                            break;
                        case "FC":
                            using (SickFCDiagnosticDataDetail detail = _context.SickFCDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                            }
                            break;
                        case "VOS":
                            using (SickVOSDiagnosticDataDetail detail = _context.SickVOSDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "FC声速偏差(<0.20 %)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "FM声速偏差(<0.20 %)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                    }
                    break;
                case "Weise":
                    switch (diagnosisType)
                    {
                        case "TT":                            
                            using (WeiseTTDiagnosticDataDetail detail = _context.WeiseTTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "温度变送器通讯", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "温度超限报警", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "PT":
                            using (WeisePTDiagnosticDataDetail detail = _context.WeisePTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "压力变送器通讯", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力超限报警", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力状态", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = detail.v2.ToString("F4") });
                            }
                            break;
                        case "FM":
                            using (WeiseFMDiagnosticDataDetail detail = _context.WeiseFMDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                int[] NotAvailable = new int[] { 1, 17, 20, 21, 22, 33, 36, 255 };
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = NotAvailable.Contains(detail.P0) ? "N/A" : detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "报警(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = NotAvailable.Contains(detail.P1) ? "N/A" : detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "正常声道数量报警(>=3)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = NotAvailable.Contains(detail.P2) ? "N/A" : detail.v2.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "标况体积流量超限", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = NotAvailable.Contains(detail.P3) ? "N/A" : detail.v3.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "质量流量超限", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = NotAvailable.Contains(detail.P4) ? "N/A" : detail.v4.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "能量流量超限", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P5).DescriptionCN, Value = NotAvailable.Contains(detail.P5) ? "N/A" : detail.v5.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "工况体积流量超限", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P6).DescriptionCN, Value = NotAvailable.Contains(detail.P6) ? "N/A" : detail.v6.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道报警", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = NotAvailable.Contains(detail.P7) ? "N/A" : detail.v7.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1信号接收率(>70)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = NotAvailable.Contains(detail.P8) ? "N/A" : detail.v8.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2信号接收率(>70)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = NotAvailable.Contains(detail.P9) ? "N/A" : detail.v9.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3信号接收率(>70)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = NotAvailable.Contains(detail.P10) ? "N/A" : detail.v10.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4信号接收率(>70)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = NotAvailable.Contains(detail.P11) ? "N/A" : detail.v11.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1A信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = NotAvailable.Contains(detail.P16) ? "N/A" : detail.v16.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1B信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P17).DescriptionCN, Value = NotAvailable.Contains(detail.P17) ? "N/A" : detail.v17.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2A信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P18).DescriptionCN, Value = NotAvailable.Contains(detail.P18) ? "N/A" : detail.v18.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2B信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P19).DescriptionCN, Value = NotAvailable.Contains(detail.P19) ? "N/A" : detail.v19.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3A信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P20).DescriptionCN, Value = NotAvailable.Contains(detail.P20) ? "N/A" : detail.v20.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3B信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P21).DescriptionCN, Value = NotAvailable.Contains(detail.P21) ? "N/A" : detail.v21.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4A信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P22).DescriptionCN, Value = NotAvailable.Contains(detail.P22) ? "N/A" : detail.v22.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4B信号增益(0.5-50000)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P23).DescriptionCN, Value = NotAvailable.Contains(detail.P23) ? "N/A" : detail.v23.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1A信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P32).DescriptionCN, Value = NotAvailable.Contains(detail.P32) ? "N/A" : detail.v32.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1B信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P33).DescriptionCN, Value = NotAvailable.Contains(detail.P33) ? "N/A" : detail.v33.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2A信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P34).DescriptionCN, Value = NotAvailable.Contains(detail.P34) ? "N/A" : detail.v34.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2B信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P35).DescriptionCN, Value = NotAvailable.Contains(detail.P35) ? "N/A" : detail.v35.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3A信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P36).DescriptionCN, Value = NotAvailable.Contains(detail.P36) ? "N/A" : detail.v36.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3B信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P37).DescriptionCN, Value = NotAvailable.Contains(detail.P37) ? "N/A" : detail.v37.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4A信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P38).DescriptionCN, Value = NotAvailable.Contains(detail.P38) ? "N/A" : detail.v38.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4B信噪比(>10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P39).DescriptionCN, Value = NotAvailable.Contains(detail.P39) ? "N/A" : detail.v39.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P48).DescriptionCN, Value = NotAvailable.Contains(detail.P48) ? "N/A" : detail.v48.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P49).DescriptionCN, Value = NotAvailable.Contains(detail.P49) ? "N/A" : detail.v49.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P50).DescriptionCN, Value = NotAvailable.Contains(detail.P50) ? "N/A" : detail.v50.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4声速偏差率(<0.2%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P51).DescriptionCN, Value = NotAvailable.Contains(detail.P51) ? "N/A" : detail.v51.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机计算声速偏差率(<0.20%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P56).DescriptionCN, Value = NotAvailable.Contains(detail.P56) ? "N/A" : detail.v56.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "剖面系数(0.9-1.1)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P57).DescriptionCN, Value = NotAvailable.Contains(detail.P57) ? "N/A" : detail.v57.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "对称性(0.9-1.1)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P58).DescriptionCN, Value = NotAvailable.Contains(detail.P58) ? "N/A" : detail.v58.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "横向流(0.9-1.1)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P59).DescriptionCN, Value = NotAvailable.Contains(detail.P59) ? "N/A" : detail.v59.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1脉动百分比(<0.1%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P60).DescriptionCN, Value = NotAvailable.Contains(detail.P60) ? "N/A" : detail.v60.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2脉动百分比(<0.1%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P61).DescriptionCN, Value = NotAvailable.Contains(detail.P61) ? "N/A" : detail.v61.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3脉动百分比(<0.1%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P62).DescriptionCN, Value = NotAvailable.Contains(detail.P62) ? "N/A" : detail.v62.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4脉动百分比(<0.1%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P63).DescriptionCN, Value = NotAvailable.Contains(detail.P63) ? "N/A" : detail.v63.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "气体漩涡角(+/-10度)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P68).DescriptionCN, Value = NotAvailable.Contains(detail.P68) ? "N/A" : detail.v68.ToString("F4") });
                            }
                            break;
                        case "FC":
                            using (WeiseFCDiagnosticDataDetail detail = _context.WeiseFCDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                            }
                            break;
                        case "VOS":
                            using (WeiseVOSDiagnosticDataDetail detail = _context.WeiseVOSDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "FC声速偏差(<0.20%)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "FM声速偏差(<0.20%)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                    }
                    break;
                case "RMG":
                    switch (diagnosisType)
                    {
                        case "TT":
                            using (RMGTTDiagnosticDataDetail detail = _context.RMGTTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "温度报警", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "PT":
                            using (RMGPTDiagnosticDataDetail detail = _context.RMGPTDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "压力报警", Result = _context.DiagnosticPTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                        case "FM":
                            using (RMGFMDiagnosticDataDetail detail = _context.RMGFMDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                int[] NotAvailable = new int[] { 1, 17, 20, 21, 22, 33, 36, 255 };
                                details.Add(new DiagnosticDataDetail() { Name = "通讯状态", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = NotAvailable.Contains(detail.P0) ? "N/A" : detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1状态(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = NotAvailable.Contains(detail.P1) ? "N/A" : detail.v1.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2状态(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = NotAvailable.Contains(detail.P2) ? "N/A" : detail.v2.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3状态(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = NotAvailable.Contains(detail.P3) ? "N/A" : detail.v3.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4状态(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = NotAvailable.Contains(detail.P4) ? "N/A" : detail.v4.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道5状态(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P5).DescriptionCN, Value = NotAvailable.Contains(detail.P5) ? "N/A" : detail.v5.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道6状态(=0)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P6).DescriptionCN, Value = NotAvailable.Contains(detail.P6) ? "N/A" : detail.v6.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1性能(>85)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = NotAvailable.Contains(detail.P7) ? "N/A" : detail.v7.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2性能(>85)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = NotAvailable.Contains(detail.P8) ? "N/A" : detail.v8.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3性能(>85)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = NotAvailable.Contains(detail.P9) ? "N/A" : detail.v9.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4性能(>85)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = NotAvailable.Contains(detail.P10) ? "N/A" : detail.v10.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道5性能(>85)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = NotAvailable.Contains(detail.P11) ? "N/A" : detail.v11.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道6性能(>85)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P12).DescriptionCN, Value = NotAvailable.Contains(detail.P12) ? "N/A" : detail.v12.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道平均性能(>85)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P13).DescriptionCN, Value = NotAvailable.Contains(detail.P13) ? "N/A" : detail.v13.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1A增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = NotAvailable.Contains(detail.P14) ? "N/A" : detail.v14.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1B增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = NotAvailable.Contains(detail.P15) ? "N/A" : detail.v15.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2A增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = NotAvailable.Contains(detail.P16) ? "N/A" : detail.v16.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2B增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P17).DescriptionCN, Value = NotAvailable.Contains(detail.P17) ? "N/A" : detail.v17.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3A增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P18).DescriptionCN, Value = NotAvailable.Contains(detail.P18) ? "N/A" : detail.v18.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3B增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P19).DescriptionCN, Value = NotAvailable.Contains(detail.P19) ? "N/A" : detail.v19.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4A增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P20).DescriptionCN, Value = NotAvailable.Contains(detail.P20) ? "N/A" : detail.v20.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4B增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P21).DescriptionCN, Value = NotAvailable.Contains(detail.P21) ? "N/A" : detail.v21.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道5A增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P22).DescriptionCN, Value = NotAvailable.Contains(detail.P22) ? "N/A" : detail.v22.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道5B增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P23).DescriptionCN, Value = NotAvailable.Contains(detail.P23) ? "N/A" : detail.v23.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道6A增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P24).DescriptionCN, Value = NotAvailable.Contains(detail.P24) ? "N/A" : detail.v24.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道6B增益偏差(<10)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P25).DescriptionCN, Value = NotAvailable.Contains(detail.P25) ? "N/A" : detail.v25.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1A信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P26).DescriptionCN, Value = NotAvailable.Contains(detail.P26) ? "N/A" : detail.v26.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1B信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P27).DescriptionCN, Value = NotAvailable.Contains(detail.P27) ? "N/A" : detail.v27.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2A信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P28).DescriptionCN, Value = NotAvailable.Contains(detail.P28) ? "N/A" : detail.v28.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2B信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P29).DescriptionCN, Value = NotAvailable.Contains(detail.P29) ? "N/A" : detail.v29.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3A信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P30).DescriptionCN, Value = NotAvailable.Contains(detail.P30) ? "N/A" : detail.v30.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3B信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P31).DescriptionCN, Value = NotAvailable.Contains(detail.P31) ? "N/A" : detail.v31.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4A信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P32).DescriptionCN, Value = NotAvailable.Contains(detail.P32) ? "N/A" : detail.v32.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4B信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P33).DescriptionCN, Value = NotAvailable.Contains(detail.P33) ? "N/A" : detail.v33.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道5A信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P34).DescriptionCN, Value = NotAvailable.Contains(detail.P34) ? "N/A" : detail.v34.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道5B信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P35).DescriptionCN, Value = NotAvailable.Contains(detail.P35) ? "N/A" : detail.v35.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道6A信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P36).DescriptionCN, Value = NotAvailable.Contains(detail.P36) ? "N/A" : detail.v36.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道6B信噪比(>20)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P37).DescriptionCN, Value = NotAvailable.Contains(detail.P37) ? "N/A" : detail.v37.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道1声速偏差率(<2.00%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P38).DescriptionCN, Value = NotAvailable.Contains(detail.P38) ? "N/A" : detail.v38.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道2声速偏差率(<2.00%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P39).DescriptionCN, Value = NotAvailable.Contains(detail.P39) ? "N/A" : detail.v39.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道3声速偏差率(<2.00%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P40).DescriptionCN, Value = NotAvailable.Contains(detail.P40) ? "N/A" : detail.v40.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道4声速偏差率(<2.00%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P41).DescriptionCN, Value = NotAvailable.Contains(detail.P41) ? "N/A" : detail.v41.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道5声速偏差率(<2.00%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P42).DescriptionCN, Value = NotAvailable.Contains(detail.P42) ? "N/A" : detail.v42.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "声道6声速偏差率(<2.00%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P43).DescriptionCN, Value = NotAvailable.Contains(detail.P43) ? "N/A" : detail.v43.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机计算声速偏差率(<5.00%)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P44).DescriptionCN, Value = NotAvailable.Contains(detail.P44) ? "N/A" : detail.v44.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "剖面系数(1.110±0.200)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P45).DescriptionCN, Value = NotAvailable.Contains(detail.P45) ? "N/A" : detail.v45.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "流速对称性(1.000±0.200)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P46).DescriptionCN, Value = NotAvailable.Contains(detail.P46) ? "N/A" : detail.v46.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "气体漩涡角1(±10.000°)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P47).DescriptionCN, Value = NotAvailable.Contains(detail.P47) ? "N/A" : detail.v47.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "气体漩涡角2(±10.000°)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P48).DescriptionCN, Value = NotAvailable.Contains(detail.P48) ? "N/A" : detail.v48.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "气体漩涡角3(±10.000°)", Result = _context.DiagnosticFMResultDescriptions.First(obj => obj.ID == detail.P49).DescriptionCN, Value = NotAvailable.Contains(detail.P49) ? "N/A" : detail.v49.ToString("F4") });
                            }
                            break;
                        case "FC":
                            using (RMGFCDiagnosticDataDetail detail = _context.RMGFCDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "流量计算机", Result = _context.DiagnosticFCResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                            }
                            break;
                        case "VOS":
                            using (RMGVOSDiagnosticDataDetail detail = _context.RMGVOSDiagnosticDataDetails.FirstOrDefault(obj => obj.ID == loopID))
                            {
                                details.Add(new DiagnosticDataDetail() { Name = "FC声速偏差(<5.00%)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P0).DescriptionCN, Value = detail.v0.ToString("F4") });
                                details.Add(new DiagnosticDataDetail() { Name = "FM声速偏差(<5.00%)", Result = _context.DiagnosticVOSResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = detail.v1.ToString("F4") });
                            }
                            break;
                    }
                    break;
            }
            return details;
        }
        public List<DiagnosticDataDetail> GetEquipmentDiagnosticDataDetailByEquipment(int equipmentID, string manufacturer)
        {
            List<DiagnosticDataDetail> details = new List<DiagnosticDataDetail>();
            switch (manufacturer)
            {
                case "ABB":
                    using (ABBGCDiagnosticDataDetail detail = _context.ABBGCDiagnosticDataDetails.First(obj => obj.ID == equipmentID))
                    {
                        details.Add(new DiagnosticDataDetail() { Name = "C1", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C3", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NC4", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "IC4", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P5).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P6).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "IC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C6", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C7", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C8", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C9", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C10", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P12).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "N2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P13).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "CO2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NEOC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "色谱状态", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = "/" });
                    }
                    break;
                case "Daniel":
                    using (DanielGCDiagnosticDataDetail detail = _context.DanielGCDiagnosticDataDetails.First(obj => obj.ID == equipmentID))
                    {
                        details.Add(new DiagnosticDataDetail() { Name = "C1", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C3", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NC4", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "IC4", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P5).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P6).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "IC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C6", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C7", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C8", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C9", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C10", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P12).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "N2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P13).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "CO2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NEOC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "报警1", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P16).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "报警2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P17).DescriptionCN, Value = "/" });
                    }
                    break;
                case "Elster":
                    using (ElsterGCDiagnosticDataDetail detail = _context.ElsterGCDiagnosticDataDetails.First(obj => obj.ID == equipmentID))
                    {
                        details.Add(new DiagnosticDataDetail() { Name = "C1", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P1).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P2).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C3", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P3).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NC4", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P4).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "IC4", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P5).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P6).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "IC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P7).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C6", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P8).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C7", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P9).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C8", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P10).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C9", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P11).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "C10", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P12).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "N2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P13).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "CO2", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P14).DescriptionCN, Value = "/" });
                        details.Add(new DiagnosticDataDetail() { Name = "NEOC5", Result = _context.DiagnosticTTResultDescriptions.First(obj => obj.ID == detail.P15).DescriptionCN, Value = "/" });
                    }
                    break;
            }
            return details;
        }

        public List<StationLoopDiagnosticData> GetLoopStatusByStations(List<int> stationIDs) 
        {
            List<StationLoopDiagnosticData> loopDiagnosticDatas = new List<StationLoopDiagnosticData>();

            loopDiagnosticDatas = (from diagnosis in _context.StationLoopDiagnosticDatas
                                   join loop in _context.StationLoops on diagnosis.ID equals loop.ID
                                   join category in _context.EquipmentCategories on loop.EquipmentCategoryID equals category.ID
                                   join status in _context.DiagnosticStatusDescriptions on diagnosis.LoopStatusID equals status.ID
                                   join station in _context.Stations.Where(station => stationIDs.Contains(station.ID)) on loop.StationID equals station.ID
                                   select new StationLoopDiagnosticData
                                   {
                                       ID = diagnosis.ID,
                                       LoopName = loop.AbbrName,
                                       LoopStatusID = diagnosis.LoopStatusID,
                                       LoopStatus = status.DescriptionCN,
                                       StationName=station.Name,
                                   }).ToList();
            return loopDiagnosticDatas;
        }
    }
}
