using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Respository;

namespace Services
{
    public class CheckService : ICheckService
    {
        private readonly ICheckRespository _respository;
        public CheckService(IConfiguration configuration, ICheckRespository checkRespository)
        {
            _respository = checkRespository;
        }

        public Task<List<HistoricalStationEquipmentCheckData>> GetStationEquipmentCheckReport(string reportCategory, string manufacturer, int equipmentID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetStationEquipmentCheckReport(reportCategory, manufacturer, equipmentID, startDateTime, endDateTime));
        }

        public Task<List<HistoricalStationLoopCheckData>> GetStationLoopCheckReport(string reportCategory, string manufacturer, int equipmentID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetStationLoopCheckReport(reportCategory, manufacturer, equipmentID, startDateTime, endDateTime));
        }

        public Task<Dictionary<string, object>> GetManualCheckData(int loopID, string manufacturer)
        {
            return Task.Run(() =>
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                int FRHisID = 0; int VOSHisID = 0; DateTime CurrentDate = DateTime.Now;
                switch (manufacturer)
                {
                    case "Daniel":
                        {
                            var realtimeFRCheckData = _respository.GetRealtimeDanielFRCheckDatas(loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeDanielVOSCheckDatas(loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDataDanielVOSChartDatas(loopID);
                            HistoricalDanielFRCheckData historicalFRCheckData = new HistoricalDanielFRCheckData();
                            HistoricalDanielVOSCheckData historicalDanielVOSCheckData = new HistoricalDanielVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalDanielFRCheckData(historicalFRCheckData, ref FRHisID);

                                EntityToEntity(realtimeVOSCheckData, historicalDanielVOSCheckData);
                                historicalDanielVOSCheckData.DateTime = CurrentDate;
                                historicalDanielVOSCheckData.ReportModeID = 1;
                                historicalDanielVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalDanielVOSCheckData(historicalDanielVOSCheckData, ref VOSHisID);

                                if (historicalDanielVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalCheckDataDanielVOSChartData> histchartdata = new List<HistoricalCheckDataDanielVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeCheckDataDanielVOSChartData realtimeCheckDataDanielVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalCheckDataDanielVOSChartData historicalCheckDataDanielVOSChartData = new HistoricalCheckDataDanielVOSChartData();
                                        EntityToEntity(realtimeCheckDataDanielVOSChartData, historicalCheckDataDanielVOSChartData);
                                        historicalCheckDataDanielVOSChartData.ReportID = VOSHisID;
                                        historicalCheckDataDanielVOSChartData.LoopID = realtimeCheckDataDanielVOSChartData.ID;
                                        historicalCheckDataDanielVOSChartData.Datetime = chartDate.AddSeconds(number * 30);
                                        histchartdata.Add(historicalCheckDataDanielVOSChartData);
                                        number++;
                                    }
                                    _respository.AddHistoricalCheckDataDanielVOSChartDatas(histchartdata);
                                }

                            }
                            break;
                        }
                    case "Elster":
                        {
                            var realtimeFRCheckData = _respository.GetRealtimeCheckDatas<RealtimeElsterFRCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeCheckDatas<RealtimeElsterVOSCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeCheckDataElsterVOSChartData>(x => x.ID == loopID);
                            HistoricalElsterFRCheckData historicalFRCheckData = new HistoricalElsterFRCheckData();
                            HistoricalElsterVOSCheckData historicalVOSCheckData = new HistoricalElsterVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalCheckDataElsterVOSChartData> histchartdata = new List<HistoricalCheckDataElsterVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeCheckDataElsterVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalCheckDataElsterVOSChartData historicalCheckDataVOSChartData = new HistoricalCheckDataElsterVOSChartData();
                                        EntityToEntity(realtimeCheckDataVOSChartData, historicalCheckDataVOSChartData);
                                        historicalCheckDataVOSChartData.ReportID = VOSHisID;
                                        historicalCheckDataVOSChartData.LoopID = realtimeCheckDataVOSChartData.ID;
                                        historicalCheckDataVOSChartData.Datetime = chartDate.AddSeconds(number * 30);
                                        histchartdata.Add(historicalCheckDataVOSChartData);
                                        number++;
                                    }
                                    _respository.AddHistoricalCheckDataChartDatas(histchartdata);
                                }
                            }
                            break;
                        }
                    case "Sick":
                        {
                            var realtimeFRCheckData = _respository.GetRealtimeCheckDatas<RealtimeSickFRCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeCheckDatas<RealtimeSickVOSCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeCheckDataSickVOSChartData>(x => x.ID == loopID);
                            HistoricalSickFRCheckData historicalFRCheckData = new HistoricalSickFRCheckData();
                            HistoricalSickVOSCheckData historicalVOSCheckData = new HistoricalSickVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalCheckDataSickVOSChartData> histchartdata = new List<HistoricalCheckDataSickVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeCheckDataSickVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalCheckDataSickVOSChartData historicalCheckDataVOSChartData = new HistoricalCheckDataSickVOSChartData();
                                        EntityToEntity(realtimeCheckDataVOSChartData, historicalCheckDataVOSChartData);
                                        historicalCheckDataVOSChartData.ReportID = VOSHisID;
                                        historicalCheckDataVOSChartData.LoopID = realtimeCheckDataVOSChartData.ID;
                                        historicalCheckDataVOSChartData.Datetime = chartDate.AddSeconds(number * 30);
                                        histchartdata.Add(historicalCheckDataVOSChartData);
                                        number++;
                                    }
                                    _respository.AddHistoricalCheckDataChartDatas(histchartdata);
                                }
                            }
                            break;
                        }
                    case "Weise":
                        {
                            var realtimeFRCheckData = _respository.GetRealtimeCheckDatas<RealtimeWeiseFRCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeCheckDatas<RealtimeWeiseVOSCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeCheckDataWeiseVOSChartData>(x => x.ID == loopID);
                            HistoricalWeiseFRCheckData historicalFRCheckData = new HistoricalWeiseFRCheckData();
                            HistoricalWeiseVOSCheckData historicalVOSCheckData = new HistoricalWeiseVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalCheckDataWeiseVOSChartData> histchartdata = new List<HistoricalCheckDataWeiseVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeCheckDataWeiseVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalCheckDataWeiseVOSChartData historicalCheckDataVOSChartData = new HistoricalCheckDataWeiseVOSChartData();
                                        EntityToEntity(realtimeCheckDataVOSChartData, historicalCheckDataVOSChartData);
                                        historicalCheckDataVOSChartData.ReportID = VOSHisID;
                                        historicalCheckDataVOSChartData.LoopID = realtimeCheckDataVOSChartData.ID;
                                        historicalCheckDataVOSChartData.Datetime = chartDate.AddSeconds(number * 30);
                                        histchartdata.Add(historicalCheckDataVOSChartData);
                                        number++;
                                    }
                                    _respository.AddHistoricalCheckDataChartDatas(histchartdata);
                                }
                            }
                            break;
                        }
                    case "RMG":
                        {
                            var realtimeFRCheckData = _respository.GetRealtimeCheckDatas<RealtimeRMGFRCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeCheckDatas<RealtimeRMGVOSCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeCheckDataRMGVOSChartData>(x => x.ID == loopID);
                            HistoricalRMGFRCheckData historicalFRCheckData = new HistoricalRMGFRCheckData();
                            HistoricalRMGVOSCheckData historicalVOSCheckData = new HistoricalRMGVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(ref historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalCheckDataRMGVOSChartData> histchartdata = new List<HistoricalCheckDataRMGVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeCheckDataRMGVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalCheckDataRMGVOSChartData historicalCheckDataVOSChartData = new HistoricalCheckDataRMGVOSChartData();
                                        EntityToEntity(realtimeCheckDataVOSChartData, historicalCheckDataVOSChartData);
                                        historicalCheckDataVOSChartData.ReportID = VOSHisID;
                                        historicalCheckDataVOSChartData.LoopID = realtimeCheckDataVOSChartData.ID;
                                        historicalCheckDataVOSChartData.Datetime = chartDate.AddSeconds(number * 30);
                                        histchartdata.Add(historicalCheckDataVOSChartData);
                                        number++;
                                    }
                                    _respository.AddHistoricalCheckDataChartDatas(histchartdata);
                                }
                            }
                            break;
                        }
                }

                var frCheckData = _respository.GetRealtimeCheckReport("FRReport", manufacturer, FRHisID);
                var vosCheckData = _respository.GetRealtimeCheckReport("VOSReport", manufacturer, VOSHisID);
                var frCheckDataDetail = _respository.GetRealtimeFRCheckData(loopID, manufacturer);
                data["FRCheckData"] = frCheckData;
                data["VOSCheckData"] = vosCheckData;
                data["FRCheckDataDetail"] = frCheckDataDetail;
                return data;
            });
        }


        /// <summary>
        /// 将一个实体的值赋值到另外一个实体
        /// </summary>
        /// <param name="objectsrc">源实体</param>
        /// <param name="objectdest">目标实体</param>
        private void EntityToEntity(object objectsrc, object objectdest)
        {
            var sourceType = objectsrc.GetType();
            var destType = objectdest.GetType();
            foreach (var source in sourceType.GetProperties())
            {
                foreach (var dest in destType.GetProperties())
                {
                    if (dest.Name == source.Name)
                    {
                        dest.SetValue(objectdest, source.GetValue(objectsrc));
                    }
                }
            }
        }
    }
}
