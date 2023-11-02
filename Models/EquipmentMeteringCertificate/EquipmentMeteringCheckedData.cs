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
        [NotMapped]
        [ExcelColumn("证书编号")]
        public string CertificateNumber { get; set; }
        [ExcelColumn("核验流量(m3/h)")]
        public string? V1 { get; set; }
        [ExcelColumn("示值误差(%)")]
        public string? V2 { get; set;}
        [ExcelColumn("重复性(%)")]
        public string? V3 { get; set;}
    }
}
