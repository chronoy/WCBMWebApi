using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("tRealtimeDiagnosticData")]
    [Keyless]
    public class StationLoopDiagnosticData
    {
        //Loop ID
        public int ID { get; set; }
        [Column("CurrentDT")]
        public DateTime DateTime { get; set; }
        [Column("FM")]
        public byte FMDiagnositcResultID { get; set; }
        [Column("TT")]
        public byte TTDiagnositcResultID { get; set; }
        [Column("PT")]
        public byte PTDiagnositcResultID { get; set; }
        [Column("FC")]
        public byte FCDiagnositcResultID { get; set; }
        [Column("VOS")]
        public byte VOSDiagnositcResultID { get; set; }
        [Column("LoopStatus")]
        public byte LoopStatusID { get; set; }
        [NotMapped]
        public string LoopName { get; set; }
        [NotMapped]
        public string Flowmeter { get; set; }
        [NotMapped]
        public string FlowmeterTypeDescription { get; set; }
        [NotMapped]
        public string FMDiagnosticResult { get; set; }
        [NotMapped]
        public string TTDiagnosticResult { get; set; }
        [NotMapped]
        public string PTDiagnosticResult { get; set; }
        [NotMapped]
        public string FCDiagnosticResult { get; set; }
        [NotMapped]
        public string VOSDiagnosticResult { get; set; }
        [NotMapped]
        public string LoopStatus { get; set; }
    }
}
