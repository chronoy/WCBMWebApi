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

namespace Services
{
    public class CheckService : ICheckService
    {
        private readonly ICheckRespository _respository;
        public CheckService(IConfiguration configuration, ICheckRespository checkRespository)
        {
            _respository = checkRespository;
        }

        public Task<List<HistoricalStationEquipmentCheckData>> GetStationEquipmentCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetStationEquipmentCheckReport(reportCategory, brandName, equipmentID, startDateTime, endDateTime));
        }

        public Task<List<HistoricalStationLoopCheckData>> GetStationLoopCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetStationLoopCheckReport(reportCategory, brandName, equipmentID, startDateTime, endDateTime));
        }

        public Task<Dictionary<string, object>> GetManualCheckData(int loopID, string brandName)
        {
            return Task.Run(() =>
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                switch (brandName)
                {
                    case "Daniel":
                        {
                            var realtimeFRCheckData = _respository.GetRealtimeDanielFRCheckDatas(loopID).FirstOrDefault();
                            var realtimeVOSCheckData = _respository.GetRealtimeDanielVOSCheckDatas(loopID).FirstOrDefault();
                            var realtimeVOSCheckChartDatas = _respository.GetRealtimeCheckDataDanielVOSChartDatas(loopID);
                            HistoricalDanielFRCheckData historicalFRCheckData = new HistoricalDanielFRCheckData();
                            HistoricalDanielVOSCheckData historicalDanielVOSCheckData = new HistoricalDanielVOSCheckData();

                            int FRHisID = 0; int VOSHisID = 0; DateTime CurrentDate = DateTime.Now;
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
                            break;
                        }
                    case "Sick":
                        {
                            break;
                        }
                    case "Weise":
                        {
                            break;
                        }
                    case "RMG":
                        {
                            break;
                        }
                }

                //IEnumerable<LoopCheckData> frCheckData =  .GetRealtimeFlowrateCheckReport(loopID, brandName);
                //IEnumerable<LoopCheckData> vosCheckData =  .GetRealtimeVOSCheckReport(loopID, brandName);
                //IEnumerable<DataItem> frCheckDataDetail =  .GetRealtimeFRCheckData(loopID, brandName);
                //data["FRCheckData"] = frCheckData.ToList();
                //data["VOSCheckData"] = vosCheckData.ToList();
                return data;
            });
        }

        public Task<Dictionary<string, object>> GetOfflineCheck(OfflineCheck offlineCheck)
        {
            return Task.Run(() =>
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
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
                data["GrossDensity"] = aga10.dRhof;
                data["StandardDensity"] = aga10.dRhob;

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
