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
        [ExcelColumn("标准物质编号")]
        public string ReferenceMaterialNumber { get; set; } = null!;
        [ExcelColumn("批次编号")]
        public string BatchNumber { get; set; } = null!;
        [ExcelColumn("定值日期")]
        public DateTime CertificationDate { get; set; }
        [ExcelColumn("有效期")]
        public DateTime ValidityPeriod { get; set; }
        [ExcelColumn("研制(生产)单位")]
        public string ReferenceMaterialProdoucer { get; set; } = null!;
        [ExcelColumn("单位地址")]
        public string Address { get; set; } = null!;
        [ExcelColumn("联系电话")]
        public string Telephone { get; set; } = null!;
        [ExcelColumn("电子邮箱")]
        public string Email { get; set; } = null!;
        [ExcelColumn("版本号")]
        public string Version { get; set; } = null!;
        [ExcelColumn("样品编号")]
        public string SampleNumber { get; set; } = null!;
        [ExcelColumn("正己烷")]
        public string Hexane { get; set; } = null!;
        [ExcelColumn("正戊烷")]
        public string Pentane { get; set; } = null!;
        [ExcelColumn("异戊烷")]
        public string Isopentane { get; set; } = null!;
        [ExcelColumn("新戊烷")]
        public string Neopentane { get; set; } = null!;
        [ExcelColumn("正丁烷")]
        public string Butane { get; set; } = null!;
        [ExcelColumn("异丁烷")]
        public string Isobutane { get; set; } = null!;
        [ExcelColumn("丙烷")]
        public string Propane { get; set; } = null!;
        [ExcelColumn("乙烷")]
        public string Ethane { get; set; } = null!;
        [ExcelColumn("二氧化碳")]
        public string CarbonDioxide { get; set; } = null!;
        [ExcelColumn("氮气")]
        public string Nitrogen { get; set; } = null!;
        [ExcelColumn("甲烷")]
        public string Methane { get; set; } = null!;
        [ExcelColumn("相对扩展不确定度")]
        public string Uncertainty { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
