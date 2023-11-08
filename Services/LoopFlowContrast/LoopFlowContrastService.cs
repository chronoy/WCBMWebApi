using Microsoft.Extensions.Configuration;
using Models;
using Respository;
using System;
using System.Collections.Generic;
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
        public LoopFlowContrastService(ILoopFlowContrastRespository respository, IConfiguration configuration, IHistoricalTrendService historicalTrendService)
        {
            _respository = respository;
            _configuration = configuration;
            _historicalTrendService = historicalTrendService;
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
    }
}
