using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface ICheckRespository
    {
        public List<HistoricalStationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, string manufacturer, int equipmentID, DateTime startDateTime, DateTime endDateTime);
        public List<HistoricalStationLoopCheckData> GetStationLoopCheckReport(string reportCategory, string manufacturer, int loopID, DateTime startDateTime, DateTime endDateTime);
        public List<HistoricalStationLoopCheckData> GetRealtimeCheckReport(string reportCategory, string manufacturer, int hisID);
        public List<RealtimeDanielFRCheckData> GetRealtimeDanielFRCheckDatas(int loopID);
        public Dictionary<string, string> GetRealtimeFRCheckData(int loopID, string manufacturer);
        public string AddHistoricalDanielFRCheckData(HistoricalDanielFRCheckData historicalDanielFRCheckData, ref int hisID);
        public List<RealtimeDanielVOSCheckData> GetRealtimeDanielVOSCheckDatas(int loopID);
        public string AddHistoricalDanielVOSCheckData(HistoricalDanielVOSCheckData historicalDanielVOSCheckData, ref int hisID);
        public List<RealtimeDanielCheckDataVOSChartData> GetRealtimeCheckDataDanielVOSChartDatas(int loopID);
        public string AddHistoricalCheckDataDanielVOSChartDatas(List<HistoricalDanielCheckDataVOSChartData> historicalCheckDataDanielVOSChartDatas);

        public List<T> GetRealtimeCheckDatas<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        public string AddHistoricalCheckData<T>(ref T entity) where T : class;
        public string AddHistoricalCheckDataChartDatas<T>(List<T> entities) where T : class;
    }
}
