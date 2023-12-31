﻿using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class CheckRespository : BaseClass, ICheckRespository
    {
        private readonly SQLServerDBContext _context;
        public CheckRespository(SQLServerDBContext context) : base(context)
        {
            _context = context;
        }
        public List<HistoricalStationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, string manufacturer, int equipmentID, DateTime startDateTime, DateTime endDateTime)
        {
            List<HistoricalStationEquipmentCheckData> reports = new List<HistoricalStationEquipmentCheckData>();
            try
            {
                switch (manufacturer)
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

        public List<HistoricalStationLoopCheckData> GetStationLoopCheckReport(string reportCategory, string manufacturer, int loopID, DateTime startDateTime, DateTime endDateTime)
        {
            List<HistoricalStationLoopCheckData> reports = new List<HistoricalStationLoopCheckData>();
            try
            {
                switch (manufacturer)
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
            catch (Exception ex)
            {
                reports = null;
            }

            return reports;
        }

        public List<HistoricalStationLoopCheckData> GetRealtimeCheckReport(string reportCategory, string manufacturer, int hisID)
        {
            List<HistoricalStationLoopCheckData> reports = new List<HistoricalStationLoopCheckData>();
            try
            {
                switch (manufacturer)
                {
                    case "Daniel":
                        {
                            switch (reportCategory)
                            {
                                case "FRReport":
                                    {
                                        reports = _context.HistoricalDanielFRCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = _context.HistoricalDanielVOSCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
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
                                        reports = _context.HistoricalElsterFRCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = _context.HistoricalElsterVOSCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
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
                                        reports = _context.HistoricalSickFRCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = _context.HistoricalSickVOSCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
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
                                        reports = _context.HistoricalWeiseFRCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = _context.HistoricalWeiseVOSCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
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
                                        reports = _context.HistoricalRMGFRCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
                                        break;
                                    }
                                case "VOSReport":
                                    {
                                        reports = _context.HistoricalRMGVOSCheckDatas.Where(checkdata => checkdata.HisID == hisID).ToList<HistoricalStationLoopCheckData>();
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
                               LoopID = report.LoopID,
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
            catch (Exception ex)
            {
                reports = null;
            }

            return reports;
        }

        public Dictionary<string, string> GetRealtimeFRCheckData(int loopID, string manufacturer)
        {
            Dictionary<string, string> result = new();
            string[] names = new string[] { "Pressure", "Temperature", "PathsVOGAverage", "C1", "C2", "C3", "nC4", "iC4", "nC5", "iC5", "NeoC5", "C6", "N2", "CO2", "GrossFlowrate", "StandardFlowrate", "MassFlowrate", "EnergyFlowrate", "CalculatedGrossFlowrate", "CalculatedStandardFlowrate", "CalculatedMassFlowrate", "CalculatedEnergyFlowrate", "GrossFlowrateDeviationRate", "StandardFlowrateDeviationRate", "MassFlowrateDeviationRate", "EnergyFlowrateDeviationRate" };
            try
            {
                switch (manufacturer)
                {
                    case "Daniel":
                        var danielFR = GetRealtimeCheckDatas<RealtimeDanielFRCheckData>(checkdata => checkdata.ID == loopID).ToList().FirstOrDefault();
                        if (danielFR != null)
                        {
                            foreach (string name in names)
                            {
                                result[name] = Convert.ToDecimal(GetPropertyValue(danielFR, name)).ToString("f2");
                            }
                        }
                        break;
                    case "Elster":
                        var elsterFR = GetRealtimeCheckDatas<RealtimeElsterFRCheckData>(checkdata => checkdata.ID == loopID).ToList().FirstOrDefault();
                        if (elsterFR != null)
                        {
                            foreach (string name in names)
                            {
                                result[name] = Convert.ToDecimal(GetPropertyValue(elsterFR, name)).ToString("f2");
                            }
                        }
                        break;
                    case "Sick":
                        var sickFR = GetRealtimeCheckDatas<RealtimeSickFRCheckData>(checkdata => checkdata.ID == loopID).ToList().FirstOrDefault();
                        if (sickFR != null)
                        {
                            foreach (string name in names)
                            {
                                result[name] = Convert.ToDecimal(GetPropertyValue(sickFR, name)).ToString("f2");
                            }
                        }
                        break;
                    case "Weise":
                        var weiseFR = GetRealtimeCheckDatas<RealtimeWeiseFRCheckData>(checkdata => checkdata.ID == loopID).ToList().FirstOrDefault();
                        if (weiseFR != null)
                        {
                            foreach (string name in names)
                            {
                                result[name] = Convert.ToDecimal(GetPropertyValue(weiseFR, name)).ToString("f2");
                            }
                        }
                        break;
                    case "RMG":
                        var rmgFR = GetRealtimeCheckDatas<RealtimeRMGFRCheckData>(checkdata => checkdata.ID == loopID).ToList().FirstOrDefault();
                        if (rmgFR != null)
                        {
                            foreach (string name in names)
                            {
                                result[name] = Convert.ToDecimal(GetPropertyValue(rmgFR, name)).ToString("f2");
                            }
                        }
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        /// <summary>
        /// 获取实时检定数据，根据传输的实体类获取不同的数据
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <returns>数据实体列表</returns>
        public List<T> GetRealtimeCheckDatas<T>(Expression<Func<T, bool>> whereLambda) where T : class
        {
            return GetEntitys(whereLambda);
        }

        /// <summary>
        /// 添加历史检定数据，根据传输的实体类往对应实体中添加数据并保存至数据库
        /// </summary>
        /// <param name="entity">历史检定数据实体类</param>
        /// <returns>添加结果</returns>
        public string AddHistoricalCheckData<T>(T entity) where T : class
        {
            return AddEntity(entity);
        }

        /// <summary>
        /// 添加历史检定数据图表数据，根据传输的实体类往对应实体中添加数据并保存至数据库
        /// </summary>
        /// <param name="entities">历史检定数据图表数据列表</param>
        /// <returns>添加结果</returns>
        public string AddHistoricalCheckDataChartDatas<T>(List<T> entities) where T : class
        {
            return AddEntitys(entities);
        }

        public string AddOnlineGCRepeatabilityCheck(List<GCRepeatabilityCheckData> entities)
        {
            return AddEntitys(entities);
        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }
    }
}
