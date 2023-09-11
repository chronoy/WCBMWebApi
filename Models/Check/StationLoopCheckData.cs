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
    public class StationLoopCheckData
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
    public class DanielVOSCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataDanielFR")]
    public class DanielFRCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataDanielLoop")]
    public class DanielLoopCheckData : StationLoopCheckData
    {
    }
    //Elster
    [Table("tHistoricalCheckDataElsterVOS")]
    public class ElsterVOSCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataElsterFR")]
    public class ElsterFRCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataElsterLoop")]
    public class ElsterLoopCheckData : StationLoopCheckData
    {
    }
    //Sick
    [Table("tHistoricalCheckDataSickVOS")]
    public class SickVOSCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataSickFR")]
    public class SickFRCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataSickLoop")]
    public class SickLoopCheckData : StationLoopCheckData
    {
    }
    //Weise
    [Table("tHistoricalCheckDataWeiseVOS")]
    public class WeiseVOSCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataWeiseFR")]
    public class WeiseFRCheckData : StationLoopCheckData
    {
    }
    [Table("tHistoricalCheckDataWeiseLoop")]
    public class WeiseLoopCheckData : StationLoopCheckData
    {
    }
}
