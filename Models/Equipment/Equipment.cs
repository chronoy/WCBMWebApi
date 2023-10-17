using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("tEquipment")]
    public class Equipment
    {
        public int ID { get; set; }
        [ExcelColumn("管线")]
        public string LineName { get; set; }
        [ExcelColumn("输气分公司")]
        public string CompanyName { get; set; }
        [ExcelColumn("安装地点")]
        public string StationName { get; set; }
        [ExcelColumn("器具名称")]
        public string Category { get; set; }
        [ExcelColumn("生产厂家")]
        public string Manufacturer { get; set; }
        [ExcelColumn("规格型号")]
        public string EquipmentModel { get; set; }
        [ExcelColumn("用户名称")]
        public string Customer { get; set; }
        [ExcelColumn("工艺位置")]
        public string? ProcessLocation { get; set; }
        [ExcelColumn("出厂编号")]
        public string SerialNumber { get; set; }
        [ExcelColumn("口径")]
        public string Caliber { get; set; }
        [ExcelColumn("量程")]
        public string? Range { get; set; }
        [ExcelColumn("准确度等级")]
        public string Accuracy { get; set; }
        [ExcelColumn("不确定度")]
        public string Uncertainty { get; set; }
        [ExcelColumn("耐压等级")]
        public string PressureLevel { get;set; }
        [ExcelColumn("实际内经(mm)")]
        public string? InsideDiameter { get; set; }
        [ExcelColumn("长度(mm)")]
        public string? Length { get; set; }
        [ExcelColumn("K-系数")]
        public string? KFactor { get; set; }
        [ExcelColumn("常用流量(m3/h)")]
        public string? CommonFlow { get; set; }
        [ExcelColumn("安装条件")]
        public string? InstallationCondition { get; set; }
        [ExcelColumn("投产日期")]
        public DateTime? ProductionDate { get; set; }
        [ExcelColumn("设备状态")]
        public string Status { get; set; }
        [ExcelColumn("贸易属性")]
        public string TradeProperty { get; set; }
        public DateTime? VerificationEndDate { get; set; }
        public int? VerificationPeriod { get; set; }
        public string? VerificationAgency { get; set; }
        public string? VerificationCertificateNumber { get; set; }
        public string? MaintenanceStatus { get; set; }
        public string? DesignDrawings { get; set; }
        [ExcelColumn("备注")]
        public string? Note { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime? ScapDate { get; set; }
    }
}
