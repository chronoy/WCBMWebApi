using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Respository;
using GasStandardFormula;
using System.Runtime.Intrinsics.Arm;
using iText.StyledXmlParser.Jsoup.Nodes;

namespace Services
{
    public class CheckService : ICheckService
    {
        private readonly ICheckRespository _respository;
        private readonly IHistoricalTrendRespository _historicalTrendRespository;
        private readonly IHistoricalTrendService _historicalTrendService;
        public CheckService(IConfiguration configuration, ICheckRespository checkRespository,IHistoricalTrendRespository historicalTrendRespository, IHistoricalTrendService historicalTrendService)
        {
            _respository = checkRespository;
            _historicalTrendRespository = historicalTrendRespository;
            _historicalTrendService = historicalTrendService;
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
                            var realtimeFRCheckData = _respository.GetRealtimeCheckDatas<RealtimeDanielFRCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeCheckDatas<RealtimeDanielVOSCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeDanielCheckDataVOSChartData>(x => x.ID == loopID);
                            HistoricalDanielFRCheckData historicalFRCheckData = new HistoricalDanielFRCheckData();
                            HistoricalDanielVOSCheckData historicalDanielVOSCheckData = new HistoricalDanielVOSCheckData();
                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalDanielVOSCheckData);
                                historicalDanielVOSCheckData.DateTime = CurrentDate;
                                historicalDanielVOSCheckData.ReportModeID = 1;
                                historicalDanielVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalDanielVOSCheckData);
                                VOSHisID = historicalDanielVOSCheckData.HisID;

                                if (historicalDanielVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalDanielCheckDataVOSChartData> histchartdata = new List<HistoricalDanielCheckDataVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeDanielCheckDataVOSChartData realtimeCheckDataDanielVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalDanielCheckDataVOSChartData historicalCheckDataDanielVOSChartData = new HistoricalDanielCheckDataVOSChartData();
                                        EntityToEntity(realtimeCheckDataDanielVOSChartData, historicalCheckDataDanielVOSChartData);
                                        historicalCheckDataDanielVOSChartData.ReportID = VOSHisID;
                                        historicalCheckDataDanielVOSChartData.LoopID = realtimeCheckDataDanielVOSChartData.ID;
                                        historicalCheckDataDanielVOSChartData.Datetime = chartDate.AddSeconds(number * 30);
                                        histchartdata.Add(historicalCheckDataDanielVOSChartData);
                                        number++;
                                    }
                                    _respository.AddHistoricalCheckDataChartDatas(histchartdata);
                                }

                            }
                            break;
                        }
                    case "Elster":
                        {
                            var realtimeFRCheckData = _respository.GetRealtimeCheckDatas<RealtimeElsterFRCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeCheckDatas<RealtimeElsterVOSCheckData>(x => x.ID == loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeElsterCheckDataVOSChartData>(x => x.ID == loopID);
                            HistoricalElsterFRCheckData historicalFRCheckData = new HistoricalElsterFRCheckData();
                            HistoricalElsterVOSCheckData historicalVOSCheckData = new HistoricalElsterVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalElsterCheckDataVOSChartData> histchartdata = new List<HistoricalElsterCheckDataVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeElsterCheckDataVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalElsterCheckDataVOSChartData historicalCheckDataVOSChartData = new HistoricalElsterCheckDataVOSChartData();
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
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeSickCheckDataVOSChartData>(x => x.ID == loopID);
                            HistoricalSickFRCheckData historicalFRCheckData = new HistoricalSickFRCheckData();
                            HistoricalSickVOSCheckData historicalVOSCheckData = new HistoricalSickVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalSickCheckDataVOSChartData> histchartdata = new List<HistoricalSickCheckDataVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeSickCheckDataVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalSickCheckDataVOSChartData historicalCheckDataVOSChartData = new HistoricalSickCheckDataVOSChartData();
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
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeWeiseCheckDataVOSChartData>(x => x.ID == loopID);
                            HistoricalWeiseFRCheckData historicalFRCheckData = new HistoricalWeiseFRCheckData();
                            HistoricalWeiseVOSCheckData historicalVOSCheckData = new HistoricalWeiseVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalWeiseCheckDataVOSChartData> histchartdata = new List<HistoricalWeiseCheckDataVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeWeiseCheckDataVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalWeiseCheckDataVOSChartData historicalCheckDataVOSChartData = new HistoricalWeiseCheckDataVOSChartData();
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
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDatas<RealtimeRMGCheckDataVOSChartData>(x => x.ID == loopID);
                            HistoricalRMGFRCheckData historicalFRCheckData = new HistoricalRMGFRCheckData();
                            HistoricalRMGVOSCheckData historicalVOSCheckData = new HistoricalRMGVOSCheckData();

                            if (realtimeFRCheckData != null && realtimeVOSCheckData != null)
                            {
                                EntityToEntity(realtimeFRCheckData, historicalFRCheckData);
                                historicalFRCheckData.DateTime = CurrentDate;
                                historicalFRCheckData.ReportModeID = 1;
                                historicalFRCheckData.LoopID = realtimeFRCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalFRCheckData);
                                FRHisID = historicalFRCheckData.HisID;

                                EntityToEntity(realtimeVOSCheckData, historicalVOSCheckData);
                                historicalVOSCheckData.DateTime = CurrentDate;
                                historicalVOSCheckData.ReportModeID = 1;
                                historicalVOSCheckData.LoopID = realtimeVOSCheckData.ID;
                                _respository.AddHistoricalCheckData(historicalVOSCheckData);
                                VOSHisID = historicalVOSCheckData.HisID;

                                if (historicalVOSCheckData.CheckDataStatusID == 0 && VOSHisID != 0)
                                {
                                    List<HistoricalRMGCheckDataVOSChartData> histchartdata = new List<HistoricalRMGCheckDataVOSChartData>();
                                    DateTime chartDate = CurrentDate.AddMinutes(-5); int number = 0;
                                    foreach (RealtimeRMGCheckDataVOSChartData realtimeCheckDataVOSChartData in realtimeVOSCheckChartDatas)
                                    {
                                        HistoricalRMGCheckDataVOSChartData historicalCheckDataVOSChartData = new HistoricalRMGCheckDataVOSChartData();
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

        public Task<Dictionary<string, object>> GetOfflineCheck(OfflineCheck offlineCheck)
        {
            return Task.Run(() =>
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                try
                {
                    GasStandardFormula.Formula Formular = new GasStandardFormula.Formula();
                    AGA10STRUCT aga10 = new AGA10STRUCT();
                    Formular.Aga10(ref aga10,
                             new double[21]{Convert.ToDouble(offlineCheck.C1),
                                    Convert.ToDouble(offlineCheck.N2),
                                    Convert.ToDouble(offlineCheck.CO2),
                                    Convert.ToDouble(offlineCheck.C2),
                                    Convert.ToDouble(offlineCheck.C3),
                                    0.0,//H2O,
                                    0.0,//H2S,
                                    0.0,//H2
                                    0.0,//CO
                                    0.0,//O2
                                    Convert.ToDouble(offlineCheck.IC4),
                                    Convert.ToDouble(offlineCheck.NC4),
                                    Convert.ToDouble(offlineCheck.IC5)+Convert.ToDouble(offlineCheck.NeoC5),
                                    Convert.ToDouble(offlineCheck.NC5),
                                    Convert.ToDouble(offlineCheck.C6),
                                    0 ,//Convert.ToDouble(condition["C7"]),
                                    0 ,//Convert.ToDouble(condition["C8"]),
                                    0,//Convert.ToDouble(condition["C9"]),
                                    0 , //Convert.ToDouble(condition["C10"]), 
                                    0.0,//He
                                    0.0//Ar
                                        },
                                     new double[2] { Convert.ToDouble(offlineCheck.Temperature),
                                             Convert.ToDouble(offlineCheck.Pressure) * 0.001}
                                    );
                    data["StandardDensity"] = aga10.dRhob;
                    data["StandardCompressFactor"] = aga10.dZb;
                    data["GrossDensity"] = aga10.dRhof; 
                    data["GrossCompressFactor"] = aga10.dZf;
                    Formular = new GasStandardFormula.Formula();
                    ISO6976Struct ISO6976 = new ISO6976Struct();
                    Formular.ISO6976(ref ISO6976, new double[21]{
                                    Convert.ToDouble(offlineCheck.C1),
                                    Convert.ToDouble(offlineCheck.N2),
                                    Convert.ToDouble(offlineCheck.CO2),
                                    Convert.ToDouble(offlineCheck.C2),
                                    Convert.ToDouble(offlineCheck.C3),
                                    0.0,//H2O,
                                    0.0,//H2S,
                                    0.0,//H2
                                    0.0,//CO
                                    0.0,//O2
                                    Convert.ToDouble(offlineCheck.IC4),
                                    Convert.ToDouble(offlineCheck.NC4),
                                    Convert.ToDouble(offlineCheck.IC5)+Convert.ToDouble(offlineCheck.NeoC5),
                                    Convert.ToDouble(offlineCheck.NC5),
                                    Convert.ToDouble(offlineCheck.C6),
                                    0 ,//Convert.ToDouble(condition["C7"]),
                                    0 ,//Convert.ToDouble(condition["C8"]),
                                    0,//Convert.ToDouble(condition["C9"]),
                                    0 , //Convert.ToDouble(condition["C10"]), 
                                    0.0,//He
                                    0.0//Ar
                                    });
                    data["CalorificValue"] = ISO6976.dCalarify;//热值
                
                }
                catch (Exception ex)
                {
                    data=new Dictionary<string, object>();
                }
                return data;

            });
        }

        public Task<List<GCRepeatabilityCheckData>> GetOnlineGCRepeatabilityCheck(int ID, List<Data> firstDatas, List<Data> secondDatas)
        {
            return Task.Run(() =>
            {
                List<GCRepeatabilityCheckData> datas = new List<GCRepeatabilityCheckData>();
                DateTime dateTime = DateTime.Now;
                for(int i=0;i<firstDatas.Count;i++)
                {
                    datas.Add(new GCRepeatabilityCheckData
                    {
                        GCID = ID,
                        DateTime = dateTime,
                        ComponentName = firstDatas[i].Name,
                        FirstValue = double.Parse(firstDatas[i].Value),
                        SecondValue = double.Parse(secondDatas[i].Value),
                        C=1.13,
                        ComponentRange= GCRepeatabilityRange(double.Parse(firstDatas[i].Value)),
                        Condition= GCRepeatabilityCondition(double.Parse(firstDatas[i].Value)),
                        Repeatability=Math.Abs(double.Parse(firstDatas[i].Value)- double.Parse(secondDatas[i].Value))/1.13,
                        Result = Math.Abs(double.Parse(firstDatas[i].Value) - double.Parse(secondDatas[i].Value)) / 1.13> GCRepeatabilityCondition(double.Parse(firstDatas[i].Value))?"不合格":"合格"
                    });
                }
                _respository.AddOnlineGCRepeatabilityCheck(datas);
                return datas;
            });
        }

        public Task<List<UnnormalizedComponentsCheckData>> GetGCUnnormalizedComponentsCheck(int equipmentID, DateTime startDateTime, string interval, string duration)
        {
            return Task.Run(async () =>
            {
                TrendTag tag = _historicalTrendRespository.GetTrendTag(equipmentID, "Equipment", "UNNORAMLIZED");
                List<string> tags =new List<string>();
                tags.Add(tag.Address);
                Dictionary<string, object> dic = await _historicalTrendService.GetHistoricalTrendsData(tags, startDateTime, interval, duration);
                List<string> times = dic["Times"] as List<string>;
                List<Dictionary<string, object>> trends = dic["Trends"] as List<Dictionary<string, object>>;
                List<float?> datas = trends[0]["TrendDatas"] as List<float?>;
                List<UnnormalizedComponentsCheckData> checkDatas = new List<UnnormalizedComponentsCheckData>() ;
                for (int i=0;i< times.Count;i++)
                {
                    UnnormalizedComponentsCheckData checkData = new UnnormalizedComponentsCheckData  { DateTime = times[i],
                                                                                                       Value= datas[i],
                                                                                                       Condition="98%-102%",
                                                                                                       Result= CheckUnnormalizedComponent(datas[i])
                                                                                                     };

                    checkDatas.Add(checkData);
                }
                return checkDatas;
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
        private string GCRepeatabilityRange(double value)
        {
            if (value < 0.1)
                return "x<0.1";
            if (value < 1)
                return "0.1≤x<1.0";
            if (value < 5)
                return "1.0≤x<5.0";
            if (value < 10)
                return "5.0≤x<10.0";
            else
                return "x>10.0";
        }
        private double GCRepeatabilityCondition(double value)
        {
            if (value < 0.1)
                return 0.01;
            if (value < 1)
                return 0.04;
            if (value < 5)
                return 0.07;
            if (value < 10)
                return 0.08;
            else
                return 0.20;
        }
        private string CheckUnnormalizedComponent(float? value)
        {
            if(value == null)
            {
                return "N/A";
            }                
            else
            {
                if(value>102 || value<98)
                {
                    return "Error";
                }
                else
                {
                    return "OK";
                }
            }
        }

    }
}
