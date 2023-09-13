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
    public class ABBGCCheckData : HistoricalStationEquipmentCheckData
    {
        [Column("GCID")]
        public int EquipmentID { get; set; }

    }
    [Table("tHistoricalCheckDataDanielGC")]
    public class DanielGCCheckData : HistoricalStationEquipmentCheckData
    {
        [Column("GCID")]
        public int EquipmentID { get; set; }
    }
    [Table("tHistoricalCheckDataElsterGC")]
    public class ElsterGCCheckData : HistoricalStationEquipmentCheckData
    {
        [Column("GCID")]
        public int EquipmentID { get; set; }
    }
}
