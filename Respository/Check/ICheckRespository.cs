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
        public Dictionary<string, string> GetRealtimeFRCheckData(int loopID, string manufacturer);
        /// <summary>
        /// 获取实时检定数据
        /// </summary>
        /// <param name="whereLambda">条件Lambda表达式</param>
        /// <returns>数据实体列表</returns>
        public List<T> GetRealtimeCheckDatas<T>(Expression<Func<T, bool>> whereLambda) where T : class;
        /// <summary>
        /// 添加历史检定数据
        /// </summary>
        /// <param name="entity">历史检定数据实体类</param>
        /// <returns>添加结果</returns>
        public string AddHistoricalCheckData<T>(T entity) where T : class;
        /// <summary>
        /// 添加历史检定数据图表数据
        /// </summary>
        /// <param name="entities">历史检定数据图表数据列表</param>
        /// <returns>添加结果</returns>
        public string AddHistoricalCheckDataChartDatas<T>(List<T> entities) where T : class;
    }
}
