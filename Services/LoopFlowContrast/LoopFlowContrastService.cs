using Microsoft.Extensions.Configuration;
using Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Respository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.IO.Image.Jpeg2000ImageData;

namespace Services
{
    public class LoopFlowContrastService : ILoopFlowContrastService
    {
        private readonly ILoopFlowContrastRespository _respository;
        private IConfiguration _configuration;
        private readonly IHistoricalTrendService _historicalTrendService;
        private readonly IStationRespository _stationRespository;
        private readonly IStationLoopRespository _loopRespository;
        public LoopFlowContrastService(ILoopFlowContrastRespository respository, 
                                        IConfiguration configuration, 
                                        IHistoricalTrendService historicalTrendService, 
                                        IStationRespository stationRespository,
                                        IStationLoopRespository loopRespository)
        {
            _respository = respository;
            _configuration = configuration;
            _historicalTrendService = historicalTrendService;
            _stationRespository = stationRespository;
            _loopRespository = loopRespository;
        }

        public Task<List<LoopFlowContrastConfig>> GetLoopFlowContrastConfigs(int stationID, List<int> contrastStates, DateTime beginDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _respository.GetLoopFlowContrastConfigs(stationID, contrastStates, beginDateTime, endDateTime));
        }

        public Task<string> AddLoopFlowContrastConfig(LoopFlowContrastConfig entity)
        {
            return Task.Run(() => _respository.AddLoopFlowContrastConfig(entity));
        }


        public Task<string> UpdateLoopFlowContrastConfig(LoopFlowContrastConfig entity)
        {
            return Task.Run(() => _respository.UpdateLoopFlowContrastConfig(entity));
        }

        public Task<bool> DeleteLoopFlowContrastConfig(int configID)
        {
            return Task.Run(() => _respository.DeleteLoopFlowContrastConfig(configID));
        }

        public Task<string> FinishLoopFlowContrastConfig(int configID)
        {
            return Task.Run(() =>
            {
                try
                {
                    var config = _respository.GetLoopFlowContrastConfig(configID);
                    LoopFlowContrastRecord record = new LoopFlowContrastRecord();
                    config.ContrastStateID = 1;
                    config.EndDateTime = DateTime.Now;
                    record.LoopFlowContrastConfigID = config.ID;
                    record.StationID = config.StationID;
                    record.InUseLoopID = config.InUseLoopID;
                    record.ContrastLoopID = config.ContrastLoopID;
                    record.StartDateTime = config.StartDateTime;
                    record.EndDateTime = config.EndDateTime.Value;

                    List<string> InUseTags = new List<string>();
                    List<string> ContrastTags = new List<string>();
                    var inUseLoopTags = _respository.GetLoopFlowContrastTags(config.InUseLoopID, _configuration["IFixNodeName"]);
                    var contrastLoopTags = _respository.GetLoopFlowContrastTags(config.ContrastLoopID, _configuration["IFixNodeName"]);
                    inUseLoopTags.ForEach(tag => InUseTags.Add(tag.Address)); contrastLoopTags.ForEach(tag => ContrastTags.Add(tag.Address));
                    List<Dictionary<string, object>> InUseData = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> ContrastData = new List<Dictionary<string, object>>();
                    InUseData = _historicalTrendService.GetHistoricalData(config.StartDateTime, "00:30:00", Duration(config.StartDateTime, config.EndDateTime.Value), InUseTags);
                    ContrastData = _historicalTrendService.GetHistoricalData(config.StartDateTime, "00:30:00", Duration(config.StartDateTime, config.EndDateTime.Value), ContrastTags);
                    Dictionary<string, List<float?>> InUseParameters = new Dictionary<string, List<float?>>();
                    Dictionary<string, List<float?>> ContrastParameters = new Dictionary<string, List<float?>>();
                    foreach (var d in InUseData)
                    {
                        if (((List<float?>)d["TrendDatas"]).Where(data => data == null).Count() > 0)
                        {
                            record.LoopContrastStatusID = 1;
                            _respository.FinishLoopFlowContrastConfig(config, record);
                            return "期间出现通信中断情况:";

                        }
                        InUseParameters.Add(inUseLoopTags.First(tag => tag.Address == d["Address"].ToString()).Name, (List<float?>)d["TrendDatas"]);

                    }
                    foreach (var d in ContrastData)
                    {
                        if (((List<float?>)d["TrendDatas"]).Where(data => data == null).Count() > 0)
                        {
                            record.LoopContrastStatusID = 1;
                            _respository.FinishLoopFlowContrastConfig(config, record);
                            return "期间出现通信中断情况:";
                        }
                        ContrastParameters.Add(contrastLoopTags.First(tag => tag.Address == d["Address"].ToString()).Name, (List<float?>)d["TrendDatas"]);
                    }

                    record.ContrastLoopStartPressure = ContrastParameters["Pressure"][1];
                    record.ContrastLoopEndPressure = ContrastParameters["Pressure"][InUseParameters["Pressure"].Count - 2];
                    record.InUseLoopStartPressure = InUseParameters["Pressure"][1];
                    record.InUseLoopEndPressure = InUseParameters["Pressure"][InUseParameters["Pressure"].Count - 2];
                    record.ContrastLoopStartTemperature = ContrastParameters["Temperature"][1];
                    record.ContrastLoopEndTemperature = ContrastParameters["Temperature"][InUseParameters["Temperature"].Count - 2];
                    record.InUseLoopStartTemperature = InUseParameters["Temperature"][1];
                    record.InUseLoopEndTemperature = InUseParameters["Temperature"][InUseParameters["Temperature"].Count - 2];
                    record.ContrastLoopStartForwordGrossCumulative = ContrastParameters["MaintenanceForwordGrossCumulative"][1];
                    record.ContrastLoopEndForwordGrossCumulative = ContrastParameters["MaintenanceForwordGrossCumulative"][InUseParameters["MaintenanceForwordGrossCumulative"].Count - 2];
                    record.InUseLoopStartForwordGrossCumulative = InUseParameters["ForwordGrossCumulative"][1];
                    record.InUseLoopEndForwordGrossCumulative = InUseParameters["ForwordGrossCumulative"][InUseParameters["ForwordGrossCumulative"].Count - 2];
                    record.ContrastLoopStartForwordStandardCumulative = ContrastParameters["MaintenanceForwordStandardCumulative"][1];
                    record.ContrastLoopEndForwordStandardCumulative = ContrastParameters["MaintenanceForwordStandardCumulative"][InUseParameters["MaintenanceForwordStandardCumulative"].Count - 2];
                    record.InUseLoopStartForwordStandardCumulative = InUseParameters["ForwordStandardCumulative"][1];
                    record.InUseLoopEndForwordStandardCumulative = InUseParameters["ForwordStandardCumulative"][InUseParameters["ForwordStandardCumulative"].Count - 2];
                    record.ContrastLoopFCCalculatedVOSDeviationRate = Math.Abs(ContrastParameters["FCCalculateVOS"][1].Value - ContrastParameters["PathsVOSAvg"][1].Value) / ContrastParameters["PathsVOSAvg"][1].Value;
                    record.InUseLoopFCCalculatedVOSDeviationRate = Math.Abs(InUseParameters["FCCalculateVOS"][1].Value - InUseParameters["PathsVOSAvg"][1].Value) / InUseParameters["PathsVOSAvg"][1].Value;
                    record.ContrastLoopVOSMaxDeviation = Math.MaxMagnitude(Math.MaxMagnitude(Math.Abs(ContrastParameters["Path1VOS"][1].Value - ContrastParameters["PathsVOSAvg"][1].Value), Math.Abs(ContrastParameters["Path2VOS"][1].Value - ContrastParameters["PathsVOSAvg"][1].Value)), Math.MaxMagnitude(Math.Abs(ContrastParameters["Path3VOS"][1].Value - ContrastParameters["PathsVOSAvg"][1].Value), Math.Abs(ContrastParameters["Path4VOS"][1].Value - ContrastParameters["PathsVOSAvg"][1].Value)));
                    record.InUseLoopVOSMaxDeviation = Math.MaxMagnitude(Math.MaxMagnitude(Math.Abs(InUseParameters["Path1VOS"][1].Value - InUseParameters["PathsVOSAvg"][1].Value), Math.Abs(InUseParameters["Path2VOS"][1].Value - InUseParameters["PathsVOSAvg"][1].Value)), Math.MaxMagnitude(Math.Abs(InUseParameters["Path3VOS"][1].Value - InUseParameters["PathsVOSAvg"][1].Value), Math.Abs(InUseParameters["Path4VOS"][1].Value - InUseParameters["PathsVOSAvg"][1].Value)));
                    if ((record.InUseLoopStartForwordGrossCumulative - record.InUseLoopEndForwordGrossCumulative) == 0 || (record.InUseLoopStartForwordStandardCumulative - record.InUseLoopEndForwordStandardCumulative) == 0)
                    {
                        record.LoopContrastStatusID = 2;
                        _respository.FinishLoopFlowContrastConfig(config, record);
                        return "除数为零:";
                    }
                    record.ForwordGrossContrastResult = Math.Abs((record.ContrastLoopEndForwordGrossCumulative - record.InUseLoopStartForwordGrossCumulative).Value - (record.ContrastLoopEndForwordGrossCumulative - record.ContrastLoopStartForwordGrossCumulative).Value) / (record.InUseLoopStartForwordGrossCumulative - record.InUseLoopEndForwordGrossCumulative);
                    record.ForwordStandardContrastResult = Math.Abs((record.ContrastLoopEndForwordStandardCumulative - record.InUseLoopStartForwordStandardCumulative).Value - (record.ContrastLoopEndForwordStandardCumulative - record.ContrastLoopStartForwordStandardCumulative).Value) / (record.InUseLoopStartForwordStandardCumulative - record.InUseLoopEndForwordStandardCumulative);
                    record.LoopContrastStatusID = 0;
                    return _respository.FinishLoopFlowContrastConfig(config, record);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            });
        }

        public Task<LoopFlowContrastRecord> GetLoopFlowContrastRecord(int configID)
        {
            return Task.Run(() => _respository.GetLoopFlowContrastRecord(configID));
        }

        private string Duration(DateTime startTime, DateTime endTime)
        {
            TimeSpan duration = endTime.Subtract(startTime);
            return duration.Days.ToString() + ":" + duration.Hours.ToString() + ":" + duration.Minutes.ToString() + ":" + duration.Seconds.ToString();
        }

        public async Task<byte[]> ExportLoopFlowContrastReport(int configID, string templatePath)
        {
            LoopFlowContrastRecord record = await GetLoopFlowContrastRecord(configID);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo existingFile = new(templatePath);
            using ExcelPackage package = new(existingFile);
            ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
            #region 写入数据
            if (record != null)
            {
                var station = _stationRespository.GetStationByID(record.StationID);
                var inUseLoop = _loopRespository.GetStationLoopByID(record.InUseLoopID);
                var contrastLoop = _loopRespository.GetStationLoopByID(record.ContrastLoopID);

                // 站名          
                workSheet.Cells[1, 1].Value = $"{station.Name}计量支路流量比对报告";

                // 设备厂家
                workSheet.Cells[2, 2].Value = inUseLoop.FlowmeterManufacturer;
                workSheet.Cells[2, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[2, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[2, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 型号
                workSheet.Cells[3, 2].Value = inUseLoop.FlowmeterModel;
                workSheet.Cells[3, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[3, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[3, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 日期
                workSheet.Cells[2, 5].Value = DateTime.Now.ToString("yyyy/MM/dd");
                workSheet.Cells[2, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[2, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[2, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路\比对支路位号
                workSheet.Cells[3, 5].Value = $"{inUseLoop.Name}\\{contrastLoop.Name}";
                workSheet.Cells[3, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[3, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[3, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[3, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路开始标况
                workSheet.Cells[7, 2].Value = record.ContrastLoopStartForwordStandardCumulative;
                workSheet.Cells[7, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[7, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[7, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[7, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路开始工况
                workSheet.Cells[7, 3].Value = record.ContrastLoopStartForwordGrossCumulative;
                workSheet.Cells[7, 3].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[7, 3].Style.Font.Name = "宋体";//字体
                workSheet.Cells[7, 3].Style.Font.Size = 10;//字体大小
                workSheet.Cells[7, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路开始压力
                workSheet.Cells[8, 2].Value = record.ContrastLoopStartPressure;
                workSheet.Cells[8, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[8, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[8, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[8, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路开始温度
                workSheet.Cells[9, 2].Value = record.ContrastLoopStartTemperature;
                workSheet.Cells[9, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[9, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[9, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[9, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路开始标况
                workSheet.Cells[7, 5].Value = record.InUseLoopStartForwordStandardCumulative;
                workSheet.Cells[7, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[7, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[7, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[7, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路开始工况
                workSheet.Cells[7, 6].Value = record.InUseLoopStartForwordGrossCumulative;
                workSheet.Cells[7, 6].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[7, 6].Style.Font.Name = "宋体";//字体
                workSheet.Cells[7, 6].Style.Font.Size = 10;//字体大小
                workSheet.Cells[7, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路开始压力
                workSheet.Cells[8, 5].Value = record.InUseLoopStartPressure;
                workSheet.Cells[8, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[8, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[8, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[8, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路开始温度
                workSheet.Cells[9, 5].Value = record.InUseLoopStartTemperature;
                workSheet.Cells[9, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[9, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[9, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[9, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对时长
                workSheet.Cells[11, 1].Value += $" {record.StartDateTime.ToString("yyyy/MM/dd HH:mm:ss")}";
                workSheet.Cells[11, 3].Value += $" {record.EndDateTime.ToString("yyyy/MM/dd HH:mm:ss")}";
                workSheet.Cells[11, 5].Value += $" {Math.Abs(record.EndDateTime.Subtract(record.StartDateTime).TotalHours)} 小时";

                // 比对支路结束标况
                workSheet.Cells[15, 2].Value = record.ContrastLoopEndForwordStandardCumulative;
                workSheet.Cells[15, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[15, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[15, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[15, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路结束工况
                workSheet.Cells[15, 3].Value = record.ContrastLoopEndForwordGrossCumulative;
                workSheet.Cells[15, 3].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[15, 3].Style.Font.Name = "宋体";//字体
                workSheet.Cells[15, 3].Style.Font.Size = 10;//字体大小
                workSheet.Cells[15, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路结束压力
                workSheet.Cells[16, 2].Value = record.ContrastLoopEndPressure;
                workSheet.Cells[16, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[16, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[16, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[16, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路结束温度
                workSheet.Cells[17, 2].Value = record.ContrastLoopEndTemperature;
                workSheet.Cells[17, 2].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[17, 2].Style.Font.Name = "宋体";//字体
                workSheet.Cells[17, 2].Style.Font.Size = 10;//字体大小
                workSheet.Cells[17, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路结束标况
                workSheet.Cells[15, 5].Value = record.InUseLoopEndForwordStandardCumulative;
                workSheet.Cells[15, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[15, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[15, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[15, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路结束工况
                workSheet.Cells[15, 6].Value = record.InUseLoopEndForwordGrossCumulative;
                workSheet.Cells[15, 6].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[15, 6].Style.Font.Name = "宋体";//字体
                workSheet.Cells[15, 6].Style.Font.Size = 10;//字体大小
                workSheet.Cells[15, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路结束压力
                workSheet.Cells[16, 5].Value = record.InUseLoopEndPressure;
                workSheet.Cells[16, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[16, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[16, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[16, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路结束温度
                workSheet.Cells[17, 5].Value = record.InUseLoopEndTemperature;
                workSheet.Cells[17, 5].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[17, 5].Style.Font.Name = "宋体";//字体
                workSheet.Cells[17, 5].Style.Font.Size = 10;//字体大小
                workSheet.Cells[17, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路声速核查结果
                workSheet.Cells[20, 4].Value = record.InUseLoopFCCalculatedVOSDeviationRate;
                workSheet.Cells[20, 4].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[20, 4].Style.Font.Name = "宋体";//字体
                workSheet.Cells[20, 4].Style.Font.Size = 10;//字体大小
                workSheet.Cells[20, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路声速核查结果
                workSheet.Cells[20, 6].Value = record.ContrastLoopFCCalculatedVOSDeviationRate;
                workSheet.Cells[20, 6].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[20, 6].Style.Font.Name = "宋体";//字体
                workSheet.Cells[20, 6].Style.Font.Size = 10;//字体大小
                workSheet.Cells[20, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 在用支路最大声速偏差
                workSheet.Cells[21, 4].Value = record.InUseLoopVOSMaxDeviation;
                workSheet.Cells[21, 4].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[21, 4].Style.Font.Name = "宋体";//字体
                workSheet.Cells[21, 4].Style.Font.Size = 10;//字体大小
                workSheet.Cells[21, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 比对支路最大声速偏差
                workSheet.Cells[21, 6].Value = record.ContrastLoopVOSMaxDeviation;
                workSheet.Cells[21, 6].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[21, 6].Style.Font.Name = "宋体";//字体
                workSheet.Cells[21, 6].Style.Font.Size = 10;//字体大小
                workSheet.Cells[21, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 工况比对结果
                workSheet.Cells[22, 3].Value = record.ForwordGrossContrastResult;
                workSheet.Cells[22, 3].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[22, 3].Style.Font.Name = "宋体";//字体
                workSheet.Cells[22, 3].Style.Font.Size = 10;//字体大小
                workSheet.Cells[22, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平

                // 标况比对结果
                workSheet.Cells[23, 3].Value = record.ForwordStandardContrastResult;
                workSheet.Cells[23, 3].Style.Font.Color.SetColor(Color.Black);//字体颜色
                workSheet.Cells[23, 3].Style.Font.Name = "宋体";//字体
                workSheet.Cells[23, 3].Style.Font.Size = 10;//字体大小
                workSheet.Cells[23, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平
            }
            #endregion

            byte[] result = package.GetAsByteArray();
            return result;
        }
    }
}
