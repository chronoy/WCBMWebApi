using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface ICheckRespository
    {
        public List<HistoricalStationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, string brandName, int equipmentID, DateTime startDateTime, DateTime endDateTime);
        public List<HistoricalStationLoopCheckData> GetStationLoopCheckReport(string reportCategory, string brandName, int loopID, DateTime startDateTime, DateTime endDateTime);
        public List<RealtimeDanielFRCheckData> GetRealtimeDanielFRCheckDatas(int loopID);
        public string AddHistoricalDanielFRCheckData(HistoricalDanielFRCheckData historicalDanielFRCheckData, ref int hisID);
        public List<RealtimeDanielVOSCheckData> GetRealtimeDanielVOSCheckDatas(int loopID);
        public string AddHistoricalDanielVOSCheckData(HistoricalDanielVOSCheckData historicalDanielVOSCheckData, ref int hisID);
        public List<RealtimeCheckDataDanielVOSChartData> GetRealtimeCheckDataDanielVOSChartDatas(int loopID);
        public string AddHistoricalCheckDataDanielVOSChartDatas(List<HistoricalCheckDataDanielVOSChartData> historicalCheckDataDanielVOSChartDatas);
    }
}
