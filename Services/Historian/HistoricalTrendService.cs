using GeFanuc.iFixToolkit.Adapter;
using Microsoft.Extensions.Configuration;
using Models;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Respository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HistoricalTrendService : IHistoricalTrendService
    {
        private readonly IHistoricalTrendRespository _historicalTrendRespository;
        private readonly string _IFixNodeName;

        public HistoricalTrendService(IConfiguration configuration,
                            IHistoricalTrendRespository historicalTrendRespository)
        {
            _historicalTrendRespository = historicalTrendRespository;
            _IFixNodeName = configuration["IFIXNodeName"];
        }

        public Task<Dictionary<string, object>> GetHistoricalTrendsData(List<string> trendTags,
                                            DateTime startDateTime,
                                            string interval,
                                            string duration)
        {
            return Task.Run(() =>
            {
                List<Dictionary<string, object>> trends = new List<Dictionary<string, object>>();
                List<string> times = new List<string>();

                List<Trend> trendInfos = _historicalTrendRespository.GetHistoricalTrend(_IFixNodeName, trendTags).ToList();
                foreach (Trend trendInfo in trendInfos)
                {
                    Dictionary<string, object> trend = new Dictionary<string, object>();
                    trend["Name"] = trendInfo.Name;
                    trend["Address"] = trendInfo.Address;
                    trend["HighLimit"] = trendInfo.HighLimit;
                    trend["LowLimit"] = trendInfo.LowLimit;
                    trend["Unit"] = trendInfo.Unit;
                    trend["Precision"] = trendInfo.Precision;
                    trend["Description"] = trendInfo.Description;
                    trends.Add(trend);
                }
                int[] intervalTimes = null;
                int count = 0;
                int groupNumber = trends.Count / 8;
                if (trends.Count % 8 > 0)
                {
                    groupNumber = groupNumber + 1;
                }
                for (int num = 0; num < groupNumber; num++)
                {
                    int err = 0;
                    int groupHandle = 0;
                    int[] tagsHandle = null;
                    string[] tags = null;
                    if (num == groupNumber - 1)
                    {
                        tagsHandle = new int[trends.Count - 8 * num];
                        tags = new string[trends.Count - 8 * num];
                        count = trends.Count - 8 * num;

                    }
                    else
                    {
                        tagsHandle = new int[8];
                        tags = new string[8];
                        count = 8;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        tags[i] = trends[8 * num + i]["Address"].ToString();
                    }
                    int[] samplesNum = new int[count];
                    Dictionary<string, object> errReturn = new Dictionary<string, object>();
                    #region "Setting"
                    if ((err = Hda.DefineGroup(out groupHandle)) != FixError.FTK_OK)
                    {
                        errReturn["Error"] = string.Format("Error defining group,Error = {0}", err);
                        return errReturn;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        err = Hda.AddNtf(groupHandle, out tagsHandle[i], tags[i]);
                        if (err != FixError.FTK_OK)
                        {
                            Hda.DeleteGroup(groupHandle);
                            //return "Error adding an NTF";
                            errReturn["Error"] = string.Format("Error adding an NTF");
                            return errReturn;
                            // return new Dictionary<string, object>();
                        }
                    }

                    if ((err = Hda.SetStart(groupHandle, startDateTime.ToString("yyyy/MM/dd"), startDateTime.ToString("HH:mm:ss"))) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        errReturn["Error"] = string.Format("Error setting start date and time");
                        return errReturn;
                        ////return "Error setting start date and time";
                        //return new Dictionary<string, object>();
                    }

                    if ((err = Hda.SetInterval(groupHandle, interval)) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        //return "";
                        errReturn["Error"] = string.Format("Error setting interval time");
                        return errReturn;
                    }

                    if ((err = Hda.SetDuration(groupHandle, duration)) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        //return "Error setting duration time";
                        errReturn["Error"] = string.Format("Error setting duration time");
                        return errReturn;
                    }


                    if ((err = Hda.Read(groupHandle, 0)) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        //return "Error reading data";
                        errReturn["Error"] = string.Format("Error reading data");
                        return errReturn;
                    }
                    #endregion

                    for (int i = 0; i < count; i++)
                    {
                        if ((err = Hda.GetNumSamples(groupHandle, tagsHandle[i], out samplesNum[i])) != FixError.FTK_OK)
                        {
                            Hda.DeleteGroup(groupHandle);
                            //return "Error getting number of samples";
                            errReturn["Error"] = string.Format("Error getting number of samples");
                            return errReturn;
                        }
                    }

                    for (int i = 0; i < count; i++)
                    {
                        float[] values = new float[samplesNum[i]];
                        intervalTimes = new int[samplesNum[i]];
                        int[] statuses = new int[samplesNum[i]];
                        int[] alarms = new int[samplesNum[i]];
                        // Read data into arrays 
                        if ((err = Hda.GetData(groupHandle, tagsHandle[i], 0, samplesNum[i], values, intervalTimes, statuses, alarms)) != FixError.FTK_OK)
                        {
                            Hda.DeleteGroup(groupHandle);
                            //return 
                            errReturn["Error"] = string.Format("Error getting data for tag: {0}", tags[i]);
                            return errReturn;
                            // return new Dictionary<string, object>();
                        }
                        List<float?> datas = new List<float?>();
                        for (int j = 0; j < statuses.Count(); j++)
                        {
                            if (statuses[j] == 0)
                            {
                                datas.Add(values[j]);
                            }
                            else
                            {
                                datas.Add(null);
                            }
                        }
                        trends[8 * num + i]["TrendDatas"] = datas;
                    }
                    Hda.DeleteGroup(groupHandle);
                }

                if (intervalTimes != null)
                {
                    foreach (int interval in intervalTimes.ToList<int>())
                    {
                        times.Add(startDateTime.AddSeconds(interval).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                Dictionary<string, object> chartData = new Dictionary<string, object>();
                chartData["Trends"] = trends;
                chartData["Times"] = times;
                return chartData;
            });
        }

        public async Task<List<Dictionary<string, object>>> GetExportHistoricalTrendsData(List<string> trendTags,
                                    DateTime startDateTime,
                                    string interval,
                                    string duration)
        {
            var chartData = await GetHistoricalTrendsData(trendTags, startDateTime, interval, duration);

            List<Dictionary<string, object>> datas = new();

            List<string> times = (List<string>)chartData["Times"];
            List<Dictionary<string, object>> trends = (List<Dictionary<string, object>>)chartData["Trends"];

            for (int i = 0; i < times.Count; i++)
            {
                Dictionary<string, object> row = new();
                row["DateTime"] = times[i].ToString();
                foreach (var trend in trends)
                {
                    List<float?> trendDatas = (List<float?>)trend["TrendDatas"];
                    row[trend["Description"].ToString()] = trendDatas[i];
                }
                datas.Add(row);
            }

            return datas;
        }

        public async Task<byte[]> ExportHistoricalTrendsDataReport(List<string> trendTags,
                                            DateTime startDateTime,
                                            string interval,
                                            string duration,
                                            string templatePath)
        {
            List<Dictionary<string, object>> historicalTrends = await GetExportHistoricalTrendsData(trendTags, startDateTime, interval, duration);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo existingFile = new(templatePath);
            using ExcelPackage package = new(existingFile);
            ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
            #region 写入数据
            if (historicalTrends.Count > 0)
            {
                List<string> keys = historicalTrends[0].Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    workSheet.Cells[1, i + 1].Style.Font.Color.SetColor(Color.Black);//字体颜色
                    workSheet.Cells[1, i + 1].Style.Font.Name = "宋体";//字体
                    workSheet.Cells[1, i + 1].Style.Font.Size = 11;//字体大小
                    workSheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平
                    workSheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 217, 196));
                    workSheet.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    if (i != 0)
                    {
                        workSheet.Cells[1, i + 1].Value = keys[i];
                    }

                    for (int j = 0; j < historicalTrends.Count; j++)
                    {
                        workSheet.Cells[j + 2, i + 1].Value = historicalTrends[j][keys[i]];
                        workSheet.Cells[j + 2, i + 1].Style.Font.Color.SetColor(Color.Black);//字体颜色
                        workSheet.Cells[j + 2, i + 1].Style.Font.Name = "宋体";//字体
                        workSheet.Cells[j + 2, i + 1].Style.Font.Size = 11;//字体大小
                        workSheet.Cells[j + 2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平
                    }
                }
            }
            #endregion

            byte[] result = package.GetAsByteArray();
            return result;
        }

        public Task<List<TrendTag>> GetTrendTags(List<int> deviceIds, List<string> deviceTypes)
        {
            return Task.Run(() => _historicalTrendRespository.GetTrendTags(deviceIds, deviceTypes));
        }

        public List<Dictionary<string, object>> GetHistoricalData(
                                      DateTime startDateTime,
                                      string interval,
                                      string duration,
                                      List<string> tagsAddress
                                      )
        {

                List<Dictionary<string, object>> trends = new List<Dictionary<string, object>>();
                //List<string> times = new List<string>();
                foreach (string tagAddress in tagsAddress)
                {
                    Dictionary<string, object> tags = new Dictionary<string, object>();
                    tags["Address"] = tagAddress;
                    trends.Add(tags);
                }
                int[] intervalTimes = null;
                int count = 0;
                int groupNumber = trends.Count / 8;
                if (trends.Count % 8 > 0)
                {
                    groupNumber = groupNumber + 1;
                }
                for (int num = 0; num < groupNumber; num++)
                {
                    int err = 0;
                    int groupHandle = 0;
                    int[] tagsHandle = null;
                    string[] tags = null;
                    if (num == groupNumber - 1)
                    {
                        tagsHandle = new int[trends.Count - 8 * num];
                        tags = new string[trends.Count - 8 * num];
                        count = trends.Count - 8 * num;

                    }
                    else
                    {
                        tagsHandle = new int[8];
                        tags = new string[8];
                        count = 8;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        tags[i] = trends[8 * num + i]["Address"].ToString();
                    }
                    int[] samplesNum = new int[count];
                    #region "Setting"
                    if ((err = Hda.DefineGroup(out groupHandle)) != FixError.FTK_OK)
                    {
                        //return string.Format("Error defining group,Error = {0}", err);
                        return new List<Dictionary<string, object>>();
                    }

                    for (int i = 0; i < count; i++)
                    {
                        err = Hda.AddNtf(groupHandle, out tagsHandle[i], tags[i]);
                        if (err != FixError.FTK_OK)
                        {
                            Hda.DeleteGroup(groupHandle);
                            //return "Error adding an NTF";
                            return new List<Dictionary<string, object>>();
                        }
                    }

                    if ((err = Hda.SetStart(groupHandle, startDateTime.ToString("yyyy/MM/dd"), startDateTime.ToString("HH:mm:ss"))) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        //return "Error setting start date and time";
                        return new List<Dictionary<string, object>>();
                    }

                    if ((err = Hda.SetInterval(groupHandle, interval)) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        //return "Error setting interval time";
                        return new List<Dictionary<string, object>>();
                    }

                    if ((err = Hda.SetDuration(groupHandle, duration)) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        //return "Error setting duration time";
                        return new List<Dictionary<string, object>>();
                    }


                    if ((err = Hda.Read(groupHandle, 0)) != FixError.FTK_OK)
                    {
                        Hda.DeleteGroup(groupHandle);
                        //return "Error reading data";
                        return new List<Dictionary<string, object>>();
                    }
                    #endregion

                    for (int i = 0; i < count; i++)
                    {
                        if ((err = Hda.GetNumSamples(groupHandle, tagsHandle[i], out samplesNum[i])) != FixError.FTK_OK)
                        {
                            Hda.DeleteGroup(groupHandle);
                            //return "Error getting number of samples";
                            return new List<Dictionary<string, object>>();
                        }
                    }

                    for (int i = 0; i < count; i++)
                    {
                        float[] values = new float[samplesNum[i]];
                        intervalTimes = new int[samplesNum[i]];
                        int[] statuses = new int[samplesNum[i]];
                        int[] alarms = new int[samplesNum[i]];
                        // Read data into arrays 
                        if ((err = Hda.GetData(groupHandle, tagsHandle[i], 0, samplesNum[i], values, intervalTimes, statuses, alarms)) != FixError.FTK_OK)
                        {
                            Hda.DeleteGroup(groupHandle);
                            //return string.Format("Error getting data for tag: {0}", tags[i]);
                            return new List<Dictionary<string, object>>();
                        }
                        List<float?> datas = new List<float?>();
                        for (int j = 0; j < statuses.Count(); j++)
                        {
                            if (statuses[j] == 0)
                            {
                                datas.Add(values[j]);
                            }
                            else
                            {
                                datas.Add(null);
                            }
                        }
                        trends[8 * num + i]["TrendDatas"] = datas;
                    }
                    Hda.DeleteGroup(groupHandle);
                }
                return trends;
        }
    }
}
