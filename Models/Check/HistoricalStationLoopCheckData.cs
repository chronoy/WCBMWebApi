using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    /// <summary>
    /// 用于区分诊断数据是Loop还是Equipment，不是诊断的具体内容
    /// </summary>
    public class HistoricalStationLoopCheckData
    {
        [Key]
        [Column("ID")]
        public int HisID { get; set; }
        public DateTime DateTime { get; set; }
        public int LoopID { get; set; }

        [Column("CheckDataStatus")]
        public int CheckDataStatusID { get; set; }
        [Column("ReportMode")]
        public int ReportModeID { get; set; }

        [NotMapped]
        public string LoopName { get; set; }
        [NotMapped]
        public string StationName { get; set; }
        [NotMapped]
        public string LineName { get; set; }
        [NotMapped]
        public string Customer { get; set; }
        [NotMapped]
        public string Manufacturer { get; set; }
        [NotMapped]
        public string Model { get; set; }
        [NotMapped]
        public string CheckDataStatus { get; set; }
        [NotMapped]
        public string ReportMode { get; set; }
    }
    //Daniel
    [Table("tHistoricalCheckDataDanielVOS")]
    public class DanielVOSCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataDanielFR")]
    public class DanielFRCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataDanielLoop")]
    public class DanielLoopCheckData : HistoricalStationLoopCheckData
    {
    }
    //Elster
    [Table("tHistoricalCheckDataElsterVOS")]
    public class ElsterVOSCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataElsterFR")]
    public class ElsterFRCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataElsterLoop")]
    public class ElsterLoopCheckData : HistoricalStationLoopCheckData
    {
    }
    //Sick
    [Table("tHistoricalCheckDataSickVOS")]
    public class SickVOSCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataSickFR")]
    public class SickFRCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataSickLoop")]
    public class SickLoopCheckData : HistoricalStationLoopCheckData
    {
    }
    //Weise
    [Table("tHistoricalCheckDataWeiseVOS")]
    public class WeiseVOSCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataWeiseFR")]
    public class WeiseFRCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataWeiseLoop")]
    public class WeiseLoopCheckData : HistoricalStationLoopCheckData
    {
    }
    //RMG
    [Table("tHistoricalCheckDataRMGVOS")]
    public class RMGVOSCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataRMGFR")]
    public class RMGFRCheckData : HistoricalStationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataRMGLoop")]
    public class RMGLoopCheckData : HistoricalStationLoopCheckData
    {
    }
}
