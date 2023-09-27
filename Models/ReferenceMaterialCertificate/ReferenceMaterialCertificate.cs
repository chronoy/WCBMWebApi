using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tReferenceMaterialCertificate")]
    public class ReferenceMaterialCertificate
    {
        [Key]
        public int ID { get; set; }
        public string ReferenceMaterialNumber { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public DateTime CertificationDate { get; set; }
        public DateTime ValidityPeriod { get; set; }
        public string ReferenceMaterialProdoucer { get; set; }
        public string Address { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string SampleNumber { get; set; } = null!;
        public string Hexane { get; set; } = null!;
        public string Pentane { get; set; } = null!;
        public string Isopentane { get; set; } = null!;
        public string Neopentane { get; set; } = null!;
        public string Butane { get; set; } = null!;
        public string Isobutane { get; set; } = null!;
        public string Propane { get; set; } = null!;
        public string Ethane { get; set; } = null!;
        public string CarbonDioxide { get; set; } = null!;
        public string Nitrogen { get; set; } = null!;
        public string Methane { get; set; } = null!;
        public string Uncertainty { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
