using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class ProductionReportRespository : IProductionReportRespository
    {
        private readonly SQLServerDBContext _context;
        public ProductionReportRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<ProductionReport> GetProductionReportData(string loopID, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                int[] loopIDs = Array.ConvertAll(loopID.Split(','), int.Parse);
                var productionReport = (from report in _context.HistoricalProductionDailyReports
                                        join loop in _context.StationLoops on report.LoopID equals loop.ID
                                        join line in _context.Lines on loop.LineID equals line.ID
                                        where loopIDs.Contains(loop.ID) && report.RptDateTime > startDateTime && report.RptDateTime < endDateTime
                                        orderby report.LoopID ascending, report.RptDateTime descending
                                        from sdcdt in _context.StationDeviceCollectDataTypes
                                        where sdcdt.ID == loop.CollectDataTypeID
                                        from station in _context.Stations
                                        where station.ID == loop.StationID
                                        select new ProductionReport
                                        {
                                            ReportDateTime = $"{report.RptDateTime:yyyy-MM-dd HH}:00:00",
                                            GrossFR = report.GrossFR == null ? null : Convert.ToDouble(report.GrossFR).ToString("f2"),
                                            StandardPreHou = sdcdt.Manufacturer == "Turbo" ? "N/A" : report.StandardPreHou == null ? null : Convert.ToDouble(report.StandardPreHou).ToString("f2"),
                                            StandardPreDay = sdcdt.Manufacturer == "Turbo" ? "N/A" : report.StandardPreDay == null ? null : Convert.ToDouble(report.StandardPreDay).ToString("f2"),
                                            StandardTotal = report.StandardTotal == null ? null : Convert.ToDouble(report.StandardTotal).ToString("f0"),
                                            HighCalorific = report.HighCalorific == null ? null : Convert.ToDouble(report.HighCalorific).ToString("f4"),
                                            LowCalorific = report.LowCalorific == null ? null : Convert.ToDouble(report.LowCalorific).ToString("f4"),
                                            EnergyFR = report.EnergyFR == null ? null : Convert.ToDouble(report.EnergyFR).ToString("f0"),
                                            EnergyCurHou = report.EnergyCurHou == null ? null : Convert.ToDouble(report.EnergyCurHou).ToString("f0"),
                                            EnergyPreHou = report.EnergyPreHou == null ? null : Convert.ToDouble(report.EnergyPreHou).ToString("f0"),
                                            EnergyCurDay = report.EnergyCurDay == null ? null : Convert.ToDouble(report.EnergyCurDay).ToString("f0"),
                                            EnergyPreDay = report.EnergyPreDay == null ? null : Convert.ToDouble(report.EnergyPreDay).ToString("f0"),
                                            EnergyTotal = report.EnergyTotal == null ? null : Convert.ToDouble(report.EnergyTotal).ToString("f0"),
                                            Manufacturer = sdcdt.Manufacturer,
                                            LoopName = loop.AbbrName,
                                            StationName = station.Name,
                                            LineName = line.Name,
                                            Customer = loop.Customer,
                                            FlowmeterModel = loop.FlowmeterModel
                                        }).ToList();
                return productionReport;
            }
            catch (Exception ex)
            {
                ex.ToString();
                List<ProductionReport> l = new List<ProductionReport>();
                return l;
            }
        }
    }
}
