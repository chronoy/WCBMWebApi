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
        [NotMapped]
        [ExcelColumn("证书编号")]
        public string? CertificateNumber { get; set; }
        [ExcelColumn("检定/校准数据1")]
        public string? V1 { get; set; }
        [ExcelColumn("检定/校准数据2")]
        public string? V2 { get; set; }
        [ExcelColumn("检定/校准数据3")]
        public string? V3 { get; set;}
        [ExcelColumn("检定/校准数据4")]
        public string? V4 { get; set;}
        [ExcelColumn("检定/校准数据5")]
        public string? V5 { get; set;}
        [ExcelColumn("检定/校准数据6")]
        public string? V6 { get; set;}
    }
}
