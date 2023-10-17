using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tEquipmentMeteringDetectingEquipment")]
    public class EquipmentMeteringDetectingEquipment
    {
        [Key]
        public int ID { get; set; }
        public int MeteringCertificateID { get; set; }
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? MeasurementRange { get; set; }
        public string? Uncertainty { get; set; }
        public string? DetectingEquipmentMeteringCertificateAgency { get; set; }
        public string? DetectingEquipmentMeteringCertificateNumber { get; set; }
        public string? DetectingEquipmentMeteringCertificateValidityDate { get; set; }
    }
}
