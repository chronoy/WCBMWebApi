using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class CheckRespository : ICheckRespository
    {
        private readonly SQLServerDBContext _context;
        public CheckRespository(SQLServerDBContext context)
        {
            _context = context;
        }
        public List<HistoricalStationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime)
        {
            List<HistoricalStationEquipmentCheckData> reports = new List<HistoricalStationEquipmentCheckData>();
            try
            {
                switch (brandName)
                {
                    case "Daniel":
                        {
                            switch (reportCategory)
                            {
                                case "GCReport":
                                    {
                                        reports = (from report in _context.HistoricalDanielGCCheckDatas.Where(checkdata => checkdata.EquipmentID == equipmentID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)
                                                   select new HistoricalStationEquipmentCheckData 
                                                   {
                                                       HisID = report.HisID,
                                                       DateTime = report.DateTime,
                                                       EquipmentID = report.EquipmentID,
                                                       CheckDataStatusID = report.CheckDataStatusID,
                                                       ReportModeID = report.ReportModeID,
                                                   }).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Elster":
                        {
                            switch (reportCategory)
                            {
                                case "GCReport":
                                    {
                                       
                                        reports = (from report in _context.HistoricalElsterGCCheckDatas.Where(checkdata => checkdata.EquipmentID == equipmentID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)
                                                   select new HistoricalStationEquipmentCheckData
                                                   {
                                                       HisID = report.HisID,
                                                       DateTime = report.DateTime,
                                                       EquipmentID = report.EquipmentID,
                                                       CheckDataStatusID = report.CheckDataStatusID,
                                                       ReportModeID = report.ReportModeID,
                                                   }).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ABB":
                        {
                            switch (reportCategory)
                            {
                                case "GCReport":
                                    {
                                        reports = (from report in _context.HistoricalABBGCCheckDatas.Where(checkdata => checkdata.EquipmentID == equipmentID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)
                                                   select new HistoricalStationEquipmentCheckData
                                                   {
                                                       HisID = report.HisID,
                                                       DateTime = report.DateTime,
                                                       EquipmentID = report.EquipmentID,
                                                       CheckDataStatusID = report.CheckDataStatusID,
                                                       ReportModeID = report.ReportModeID,
                                                   }).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                }
                reports = (from report in reports
                           join stationEquipment in _context.StationEquipments
                           on report.EquipmentID equals stationEquipment.ID
                           join station in _context.Stations
                           on stationEquipment.StationID equals station.ID
                           join checkStatus in _context.CheckDescriptionStatuses
                           on report.CheckDataStatusID equals checkStatus.ID
                           select new HistoricalStationEquipmentCheckData
                           {
                               HisID = report.HisID,
                               DateTime = report.DateTime,
                               EquipmentID = report.EquipmentID,
                               CheckDataStatusID = report.CheckDataStatusID,
                               ReportModeID = report.ReportModeID,
                               EquipmentName = stationEquipment.Name,
                               StationName = station.Name,
                               Manufacturer = stationEquipment.Manufacturer,
                               Model = stationEquipment.Model,
                               ReportMode = report.ReportModeID == 1 ? "手动" : "自动",
                               CheckDataStatus = checkStatus.Description
                           }).ToList();

                return reports;
            }
            catch (Exception ex)
            {
                return null;
            }
            
         
        }

        public List<HistoricalStationLoopCheckData> GetStationLoopCheckReport(string reportCategory, string brandName, int loopID, DateTime startDateTime, DateTime endDateTime)
        {
            List<HistoricalStationLoopCheckData> reports = new List<HistoricalStationLoopCheckData>();
            try
            {
                switch (brandName)
                {
                    case "Daniel":
                        {
                            switch (reportCategory)
                            {
                                case "FRReport":
                                    {
                                        reports = (_context.HistoricalDanielFRCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();                                       
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = (_context.HistoricalDanielVOSCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "LoopReport":
                                    {
                                        reports = (_context.HistoricalDanielLoopCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Elster":
                        {
                            switch (reportCategory)
                            {
                                case "FRReport":
                                    {
                                        reports = (_context.HistoricalElsterFRCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = (_context.HistoricalElsterVOSCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                     
                                    }
                                case "LoopReport":
                                    {
                                        reports = (_context.HistoricalElsterLoopCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Sick":
                        {
                            switch (reportCategory)
                            {
                                case "FRReport":
                                    {
                                        reports = (_context.HistoricalSickFRCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = (_context.HistoricalSickVOSCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "LoopReport":
                                    {
                                        reports = (_context.HistoricalSickLoopCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "Weise":
                        {
                            switch (reportCategory)
                            {
                                case "FRReport":
                                    {
                                        reports = (_context.HistoricalWeiseFRCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = (_context.HistoricalWeiseVOSCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "LoopReport":
                                    {
                                        reports = (_context.HistoricalWeiseLoopCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "RMG":
                        {
                            switch (reportCategory)
                            {
                                case "FRReport":
                                    {
                                        reports = (_context.HistoricalRMGFRCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = (_context.HistoricalRMGVOSCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "LoopReport":
                                    {
                                        reports = (_context.HistoricalRMGLoopCheckDatas.Where(checkdata => checkdata.LoopID == loopID && DateTime.Compare(checkdata.DateTime, startDateTime) >= 0 && DateTime.Compare(checkdata.DateTime, endDateTime) <= 0)).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                            }
                            break;
                        }
                }

                reports = (from report in reports
                           join loop in _context.StationLoops
                           on report.LoopID equals loop.ID
                           join station in _context.Stations
                           on loop.StationID equals station.ID
                           join line in _context.Lines
                           on loop.LineID equals line.ID
                           join checkStatus in _context.CheckDescriptionStatuses
                           on report.CheckDataStatusID equals checkStatus.ID
                           select new HistoricalStationLoopCheckData
                           {
                               HisID = report.HisID,
                               DateTime = report.DateTime,
                               LoopID = loopID,
                               CheckDataStatusID = report.CheckDataStatusID,
                               ReportModeID = report.ReportModeID,
                               LoopName = loop.Name,
                               StationName = station.Name,
                               LineName = line.Name,
                               Customer = loop.Customer,
                               Manufacturer = loop.FlowComputerManufacturer,
                               Model = loop.FlowComputerModel,
                               ReportMode = report.ReportModeID == 1 ? "手动" : "自动",
                               CheckDataStatus = checkStatus.Description
                           }).ToList();
            }
            catch (Exception ex )
            {
                reports = null;
            }
         
            return reports;

        }

        public List<RealtimeDanielFRCheckData> GetRealtimeDanielFRCheckDatas(int loopID) 
        {
           return _context.RealtimeDanielFRCheckDatas.Where(realtimecheck => realtimecheck.ID == loopID).ToList();
        }

        public string AddHistoricalDanielFRCheckData(HistoricalDanielFRCheckData historicalDanielFRCheckData ,ref int hisID)
        {
            using (var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _context.HistoricalDanielFRCheckDatas.Add(historicalDanielFRCheckData);
                    _context.SaveChanges();
                    hisID = historicalDanielFRCheckData.HisID; 
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                   
                        return "OtherError";
                }
                return "OK";
            }
        }

        public List<RealtimeDanielVOSCheckData> GetRealtimeDanielVOSCheckDatas(int loopID)
        {
            return _context.RealtimeDanielVOSCheckDatas.Where(realtimecheck => realtimecheck.ID == loopID).ToList();
        }

        public string AddHistoricalDanielVOSCheckData(HistoricalDanielVOSCheckData historicalDanielVOSCheckData, ref int hisID)
        {
            using (var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _context.HistoricalDanielVOSCheckDatas.Add(historicalDanielVOSCheckData);
                    _context.SaveChanges();
                    hisID = historicalDanielVOSCheckData.HisID;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return "OtherError";
                }
                return "OK";
            }
        }

        public List<RealtimeCheckDataDanielVOSChartData> GetRealtimeCheckDataDanielVOSChartDatas(int loopID)
        {
            return _context.RealtimeCheckDataDanielVOSChartDatas.Where(realtimeCheckChartData => realtimeCheckChartData.ID == loopID).ToList();
        }

        public string AddHistoricalCheckDataDanielVOSChartDatas(List<HistoricalCheckDataDanielVOSChartData> historicalCheckDataDanielVOSChartDatas)
        {
            using (var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _context.HistoricalCheckDataDanielVOSChartDatas.AddRange(historicalCheckDataDanielVOSChartDatas);
                    _context.SaveChanges(); 
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return "OtherError";
                }
                return "OK";
            }
        }
    }
}
