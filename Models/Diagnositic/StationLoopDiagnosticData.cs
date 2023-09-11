using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Keyless]
    public class StationLoopDiagnosticData
    {
        //Loop ID
        public int ID { get; set; }
        [Column("CurrentDT")]
        public string DateTime { get; set; }
        [Column("FM")]
        public int FMDiagnositcResultID { get; set; }
        [Column("TT")]
        public int TTDiagnositcResultID { get; set; }
        [Column("PT")]
        public int PTDiagnositcResultID { get; set; }
        [Column("FC")]
        public int FCDiagnositcResultID { get; set; }
        [Column("VOS")]
        public int VOSDiagnositcResultID { get; set; }
        [Column("LoopStatus")]
        public int LoopStatusID { get; set; }
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
    }
}
