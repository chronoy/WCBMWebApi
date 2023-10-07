using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tEquipmentMeteringCheckedData")]
    public class EquipmentMeteringCheckedData
    {
        [Key]
        public int ID { get; set; }
        public int MeteringCertificateID { get; set; }
        public string? V1 { get; set; }
        public string? V2 { get; set;}
        public string? V3 { get; set;}
    }
}
