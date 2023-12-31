﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Keyless]
    public class Alarm
    {
        [Column("ALM_NATIVETIMEIN")]
        public DateTime StartTime { get; set; }
        [Column("ALM_NATIVETIMELAST")]
        public DateTime EndTime { get; set; }
        [Column("ALM_PHYSLNODE")]
        public string NodeName { get; set; }
        [Column("ALM_TAGNAME")]
        public string TagName { get; set; }
        [Column("ALM_VALUE")]
        public string Value { get; set; }
        [Column("ALM_MSGTYPE")]
        public string MessageType { get; set; }
        [Column("ALM_DESCR")]
        public string Description { get; set; }
        [Column("ALM_ALMPRIORITY")]
        public string Priority { get; set; }
        [Column("ALM_ALMSTATUS")]
        public string Status { get; set; }
        [Column("ALM_ALMAREA")]
        public string Area { get; set; }
        [Column("ALM_OPNAME")]
        public string OperatorName { get; set; }
        [Column("ALM_OPFULLNAME")]
        public string FullOperatorName { get; set; }

    }

    [Table("tRealtimeAlarm")]
    public class RealtimeAlarm 
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [Column("ALM_NATIVETIMEIN")]
        public DateTime StartTime { get; set; }
        [Column("ALM_NATIVETIMELAST")]
        public DateTime EndTime { get; set; }
        [Column("ALM_PHYSLNODE")]
        public string? NodeName { get; set; }
        [Column("ALM_TAGNAME")]
        public string? TagName { get; set; }
        [Column("ALM_VALUE")]
        public string? Value { get; set; }
        [Column("ALM_MSGTYPE")]
        public string? MessageType { get; set; }
        [Column("ALM_DESCR")]
        public string? Description { get; set; }
        [Column("ALM_ALMPRIORITY")]
        public string? Priority { get; set; }
        [Column("ALM_ALMSTATUS")]
        public string? Status { get; set; }
        [Column("ALM_ALMAREA")]
        public string? Area { get; set; }
        [Column("ALM_OPNAME")]
        public string? OperatorName { get; set; }
        [Column("ALM_OPFULLNAME")]
        public string? FullOperatorName { get; set; }
        [Column("ALM_ACKED")]
        public string? ACKED { get; set; }
        [NotMapped]
        public string DeviceArea { get; set; }
        [NotMapped]
        public string ManufacturerArea { get; set; }
        [NotMapped]
        public string Grade { get; set; }   
    }

    [Table("tHistoricalAlarm")]
    public class HistoricalAlarm 
    {
        [Key]
        [Column("ID")]
        public int HisID { get; set; }
        [Column("ALM_NATIVETIMEIN")]
        public DateTime StartTime { get; set; }
        [Column("ALM_NATIVETIMELAST")]
        public DateTime EndTime { get; set; }
        [Column("ALM_PHYSLNODE")]
        public string? NodeName { get; set; }
        [Column("ALM_TAGNAME")]
        public string? TagName { get; set; }
        [Column("ALM_VALUE")]
        public string? Value { get; set; }
        [Column("ALM_MSGTYPE")]
        public string? MessageType { get; set; }
        [Column("ALM_DESCR")]
        public string? Description { get; set; }
        [Column("ALM_ALMPRIORITY")]
        public string? Priority { get; set; }
        [Column("ALM_ALMSTATUS")]
        public string? Status { get; set; }
        [Column("ALM_ALMAREA")]
        public string? Area { get; set; }
        [Column("ALM_OPNAME")]
        public string? OperatorName { get; set; }
        [Column("ALM_OPFULLNAME")]
        public string? FullOperatorName { get; set; }
        [NotMapped]
        public string Grade { get; set; }
    }

    public class HistoricalStatisticalAlarm
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int Count { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }

    }
}
