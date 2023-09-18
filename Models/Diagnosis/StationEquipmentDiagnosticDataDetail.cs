using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StationEquipmentDiagnosticDataDetail
    {
    }
    [Table("tRealtimeDiagnosticDataABBGC")]
    public class ABBGCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public void Dispose()
        {

        }
    }
    [Table("tRealtimeDiagnosticDataDanielGC")]
    public class DanielGCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public byte P17 { get; set; }
        public void Dispose()
        {

        }
    }

    [Table("tRealtimeDiagnosticDataElsterGC")]
    public class ElsterGCDiagnosticDataDetail : IDisposable
    {
        [Key]
        public int ID { get; set; }
        public byte P0 { get; set; }
        public byte P1 { get; set; }
        public byte P2 { get; set; }
        public byte P3 { get; set; }
        public byte P4 { get; set; }
        public byte P5 { get; set; }
        public byte P6 { get; set; }
        public byte P7 { get; set; }
        public byte P8 { get; set; }
        public byte P9 { get; set; }
        public byte P10 { get; set; }
        public byte P11 { get; set; }
        public byte P12 { get; set; }
        public byte P13 { get; set; }
        public byte P14 { get; set; }
        public byte P15 { get; set; }
        public byte P16 { get; set; }
        public byte P17 { get; set; }
        public void Dispose()
        {

        }
    }
}
