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
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }
    [Table("tDiagnosticDescriptionPT")]
    public class DiagnosticPTResultDescription : DiagnosticResultDescription
    {
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }
    [Table("tDiagnosticDescriptionFM")]
    public class DiagnosticFMResultDescription : DiagnosticResultDescription
    {
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }
    [Table("tDiagnosticDescriptionFC")]
    public class DiagnosticFCResultDescription : DiagnosticResultDescription
    {
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }
    [Table("tDiagnosticDescriptionVOS")]
    public class DiagnosticVOSResultDescription : DiagnosticResultDescription
    {
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }
    [Table("tDiagnosticDescriptionGC")]
    public class DiagnosticGCResultDescription : DiagnosticResultDescription
    {
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }
}
