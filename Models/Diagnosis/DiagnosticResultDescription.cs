using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tDiagnosticDescriptionResult")]
    public class DiagnosticResultDescription
    {
        [Key]
        public byte ID {  get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }

    [Table("tDiagnosticDescriptionTT")]
    public class DiagnosticTTResultDescription: DiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionPT")]
    public class DiagnosticPTResultDescription : DiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionFM")]
    public class DiagnosticFMResultDescription : DiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionFC")]
    public class DiagnosticFCResultDescription : DiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionVOS")]
    public class DiagnosticVOSResultDescription : DiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionGC")]
    public class DiagnosticGCResultDescription : DiagnosticResultDescription
    {
    }
}
