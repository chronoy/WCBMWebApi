using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class HistoricalStationEquipmentCheckData
    {
        [Key]
        [Column("ID")]
        public int HisID { get; set; }
        public DateTime DateTime { get; set; }
        public int EquipmentID { get; set; }

        [Column("CheckDataStatus")]
        public int CheckDataStatusID { get; set; }
        [Column("ReportMode")]
        public int ReportModeID { get; set; }

        [NotMapped]
        public string EquipmentName { get; set; }
        [NotMapped]
        public string StationName { get; set; }
        [NotMapped]
        public string Manufacturer { get; set; }
        [NotMapped]
        public string Model { get; set; }
        [NotMapped]
        public string CheckDataStatus { get; set; }
        [NotMapped]
        public string ReportMode { get; set; }
    }

    [Table("tHistoricalCheckDataABBGC")]
    public class HistoricalABBGCCheckData : HistoricalStationEquipmentCheckData
    {
        [Column("GCID")]
        public int EquipmentID { get; set; }

    }
    [Table("tHistoricalCheckDataDanielGC")]
    public class HistoricalDanielGCCheckData : HistoricalStationEquipmentCheckData
    {
        [Column("GCID")]
        public int EquipmentID { get; set; }
    }
    [Table("tHistoricalCheckDataElsterGC")]
    public class HistoricalElsterGCCheckData : HistoricalStationEquipmentCheckData
    {
        [Column("GCID")]
        public int EquipmentID { get; set; }
    }

    [Table("tHistoricalGCRepeatabilityCheckData")]
    public class GCRepeatabilityCheckData
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [Column("DateTime")]
        public DateTime DateTime { get; set; }
        [Column("GCID")]
        public int GCID { get; set; }
        [Column("ComponentName")]
        public string ComponentName { get; set; }
        [Column("FirstValue")]
        public double FirstValue { get; set; }
        [Column("SecondValue")]
        public double SecondValue { get; set; }
        [Column("C")]
        public double C { get; set; }
        [Column("ComponentRange")]
        public string ComponentRange { get; set; }
        [Column("Repeatability")]
        public double Repeatability { get; set; }
        [Column("Condition")]
        public double Condition { get; set; }
        [Column("Result")]
        public string Result { get; set; }
    }
}
