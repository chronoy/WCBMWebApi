using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tDiagnosticDescriptionStatus")]
    public class DiagnosticStatusDescription
    {
        [Key]
        public byte ID { get; set; }
        public string DescriptionCN { get; set; }
        public string? DescriptionRU { get; set; }
        public string? DescriptionEN { get; set; }
    }
}
