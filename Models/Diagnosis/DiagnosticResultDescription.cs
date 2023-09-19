using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseDiagnosticResultDescription
    {
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }

    [Table("tDiagnosticDescriptionResult")]
    public class DiagnosticResultDescription: BaseDiagnosticResultDescription
    {
    }

    [Table("tDiagnosticDescriptionTT")]
    public class DiagnosticTTResultDescription: BaseDiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionPT")]
    public class DiagnosticPTResultDescription : BaseDiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionFM")]
    public class DiagnosticFMResultDescription : BaseDiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionFC")]
    public class DiagnosticFCResultDescription : BaseDiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionVOS")]
    public class DiagnosticVOSResultDescription : BaseDiagnosticResultDescription
    {
    }
    [Table("tDiagnosticDescriptionGC")]
    public class DiagnosticGCResultDescription : BaseDiagnosticResultDescription
    {
    }
}
