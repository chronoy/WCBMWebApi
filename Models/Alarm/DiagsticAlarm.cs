using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{

    [Keyless]
    [Table("tRealtimeDiagnosticAlarm")]
    public class DiagnosticAlarm
    {
        [Column("ALM_NATIVETIMEIN")]
        public DateTime StartTime { get; set; }
        [Column("ALM_NATIVETIMELAST")]
        public DateTime EndTime { get; set; }
        [Column("ALM_PHYSLNODE")]
        public string NodeName { get; set; }
        [Column("ALM_TAGNAME")]
        public string TagName { get; set; }
        [Column("ALM_DESCR")]
        public string Description { get; set; }
        [Column("ALM_VALUE")]
        public string DiagnosticResult { get; set; }
        [Column("ALM_CURRENTVALUE")]
        public double Value { get; set; }
        [Column("ALM_ALMSTATUS")]
        public string Status { get; set; }
        [Column("ALM_MSGTYPE")]
        public string MessageType { get; set; }
        [Column("ALM_ALMPRIORITY")]
        public string Priority { get; set; }
        [Column("ALM_LOOPID")]
        public int LoopID { get; set; }
        [Column("ALM_BRAND")]
        public string Brand { get; set; }
        [Column("ALM_DEVICE")]
        public string Device { get; set; }
    }
}
