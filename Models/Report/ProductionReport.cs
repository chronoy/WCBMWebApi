using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Keyless]
    public class ProductionReport
    {
        public string StationName { get; set; }
        public string LoopName { get; set; }
        public string Brand { get; set; }
        public string LineName { get; set; }
        public string FlowmeterModel { get; set; }
        public string Customer { get; set; }
        public string ReportDateTime { get; set; }
        public string? GrossFR { get; set; }
        public string? StandardPreHou { get; set; }
        public string? StandardPreDay { get; set; }
        public string? StandardTotal { get; set; }
        public string? HighCalorific { get; set; }
        public string? LowCalorific { get; set; }
        public string? EnergyFR { get; set; }
        public string? EnergyCurHou { get; set; }
        public string? EnergyPreHou { get; set; }
        public string? EnergyCurDay { get; set; }
        public string? EnergyPreDay { get; set; }
        public string? EnergyTotal { get; set; }
    }

    [Table("tHistoricalProductionDailyReport")]
    public class HistoricalProductionDailyReport
    {
        public int ID { get; set; }
        public int LoopID { get; set; }
        public DateTime RptDateTime { get; set; }
        public double? GrossFR { get; set; }
        public double? StandardPreHou { get; set; }
        public double? StandardPreDay { get; set; }
        public double? StandardTotal { get; set; }
        public double? HighCalorific { get; set; }
        public double? LowCalorific { get; set; }
        public double? EnergyFR { get; set; }
        public double? EnergyCurHou { get; set; }
        public double? EnergyPreHou { get; set; }
        public double? EnergyCurDay { get; set; }
        public double? EnergyPreDay { get; set; }
        public double? EnergyTotal { get; set; }
    }
}
