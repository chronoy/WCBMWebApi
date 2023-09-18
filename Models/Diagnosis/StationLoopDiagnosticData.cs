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

    [Table("tRealtimeDiagnosticDataDanielFM")]
    [Keyless]
    public class RealtimeDanielFMDiagnosticData
    {
        public int ID { get; set; }
        public int P0 { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int P3 { get; set; }
        public int P4 { get; set; }
        public int P5 { get; set; }
        public int P6 { get; set; }
        public int P7 { get; set; }
        public int P8 { get; set; }
        public int P9 { get; set; }
        public int P10 { get; set; }
        public int P11 { get; set; }
        public int P12 { get; set; }
        public int P13 { get; set; }
        public int P14 { get; set; }
        public int P15 { get; set; }
        public int P16 { get; set; }
        public int P17 { get; set; }
        public int P18 { get; set; }
        public int P19 { get; set; }
        public int P20 { get; set; }
        public int P21 { get; set; }
        public int P22 { get; set; }
        public int P23 { get; set; }
        public int P24 { get; set; }
        public int P25 { get; set; }
        public int P26 { get; set; }
        public int P27 { get; set; }
        public int P28 { get; set; }
        public int P29 { get; set; }
        public int P30 { get; set; }
        public int P31 { get; set; }
        public int P32 { get; set; }
        public int P33 { get; set; }
        public int P34 { get; set; }
        public int P35 { get; set; }
        public int P36 { get; set; }
        public int P37 { get; set; }
        public int P38 { get; set; }
        public int P39 { get; set; }
        public int P40 { get; set; }
        public double v0 { get; set; }
        public double v1 { get; set; }
        public double v2 { get; set; }
        public double v3 { get; set; }
        public double v4 { get; set; }
        public double v5 { get; set; }
        public double v6 { get; set; }
        public double v7 { get; set; }
        public double v8 { get; set; }
        public double v9 { get; set; }
        public double v10 { get; set; }
        public double v11 { get; set; }
        public double v12 { get; set; }
        public double v13 { get; set; }
        public double v14 { get; set; }
        public double v15 { get; set; }
        public double v16 { get; set; }
        public double v17 { get; set; }
        public double v18 { get; set; }
        public double v19 { get; set; }
        public double v20 { get; set; }
        public double v21 { get; set; }
        public double v22 { get; set; }
        public double v23 { get; set; }
        public double v24 { get; set; }
        public double v25 { get; set; }
        public double v26 { get; set; }
        public double v27 { get; set; }
        public double v28 { get; set; }
        public double v29 { get; set; }
        public double v30 { get; set; }
        public double v31 { get; set; }
        public double v32 { get; set; }
        public double v33 { get; set; }
        public double v34 { get; set; }
        public double v35 { get; set; }
        public double v36 { get; set; }
        public double v37 { get; set; }
        public double v38 { get; set; }
        public double v39 { get; set; }
        public double V40 { get; set; }
        public double c0 { get; set; }
        public double c1 { get; set; }
        public double c2 { get; set; }
        public double c3 { get; set; }


    }
}
