using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tEquipmentMeteringResultData")]
    public class EquipmentMeteringResultData
    {
        [Key]
        public int ID { get; set; }
        public int MeteringCertificateID { get; set; }
        public string? V1 { get; set; }
        public string? V2 { get; set; }
        public string? V3 { get; set;}
        public string? V4 { get; set;}
        public string? V5 { get; set;}
        public string? V6 { get; set;}
    }
}
