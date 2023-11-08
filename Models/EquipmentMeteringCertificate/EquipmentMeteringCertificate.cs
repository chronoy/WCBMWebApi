using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tEquipmentMeteringCertificate")]
    public class EquipmentMeteringCertificate
    {
        [Key]
        public int ID { get; set; }
        [ExcelColumn("机构")]
        public string Agency { get; set; }
        [ExcelColumn("证书编号")]
        public string CertificateNumber { get; set; }
        [ExcelColumn("客户名称")]
        public string? Customer { get; set; }
        [ExcelColumn("客户联系方式")]
        public string? CustomerContact { get; set; }
        [ExcelColumn("客户地址")]
        public string? CustomerAddress { get; set; }
        [ExcelColumn("器具名称")]
        public string? EquipmentCategory { get; set; }
        [ExcelColumn("制造厂商")]
        public string? EquipmentManufacturer { get; set; }
        [ExcelColumn("型号规格")]
        public string? EquipmentModel { get; set; }
        [ExcelColumn("出厂编号")]
        public string? EquipmentSerialNumber { get; set; }
        [ExcelColumn("准确度等级")]
        public string? EquipmentAccuracy { get; set; }
        [ExcelColumn("检定结论")]
        public string? CertificateConclusion { get; set; }
        [ExcelColumn("批准人")]
        public string? ApprovedBy { get; set; }
        [ExcelColumn("批准人职务")]
        public string? ApproverPosition { get; set; }
        [ExcelColumn("核验员")]
        public string? CheckedBy { get; set; }
        [ExcelColumn("检定/校准员")]
        public string? VerifiedBy { get; set; }
        public string? CelibratedBy { get; set; }
        [ExcelColumn("接收日期")]
        public DateTime? ReceiptDate { get; set; }
        [ExcelColumn("检定/校准日期")]
        public DateTime VerificationDate { get; set; }
        [ExcelColumn("有效期至")]
        public DateTime ValidityDate { get; set; }
        [ExcelColumn("签发/批准日期")]
        public DateTime? AuthorizationDate { get; set; }
        public DateTime? CelibrationDate { get; set; }
        public DateTime? IssueDate { get; set; }
        [ExcelColumn("依据/参照文件")]
        public string? ReferenceRegulation { get; set; }
        [ExcelColumn("检定介质")]
        public string? ENVMedium { get; set; }
        [ExcelColumn("检定流量(m3/h)")]
        public string? ENVFlowrate { get; set; }
        [ExcelColumn("介质温度(℃)")]
        public string? ENVMediumTemperature { get; set; }
        [ExcelColumn("环境温度(℃)")]
        public string? ENVEnvironmentTemperature { get; set; }
        [ExcelColumn("介质压力(mpa)")]
        public string? ENVMediumPerssure { get; set; }
        [ExcelColumn("环境压力(kPa)")]
        public string? ENVEnvironmentPressure { get; set; }
        public string? ENVMediumHumidity { get; set; }
        [ExcelColumn("湿度(%RH)")]
        public string? ENVEnvironmentHumidity { get; set; }
        public string? ENVRelativeHumidity { get; set; }
        [ExcelColumn("安装条件")]
        public string? ENVInstallationCondition { get; set; }
        [ExcelColumn("安装方向")]
        public string? ENVInstallationDirection { get; set; }
        [ExcelColumn("限制使用条件")]
        public string? ENVRestrictiveCondition { get; set; }
        [ExcelColumn("限制测量范围")]
        public string? ENVRestrictiveMeasurementRange { get; set; }
        [ExcelColumn("地点")]
        public string? ENVLocation { get; set; }
        [ExcelColumn("N2")]
        public string? ENVN2 { get; set; }
        [ExcelColumn("CH4")]
        public string? ENVCH4 { get; set; }
        [ExcelColumn("CO2")]
        public string? ENVCO2 { get; set; }
        [ExcelColumn("C2H6")]
        public string? ENVC2H6 { get; set; }
        [ExcelColumn("C3H8")]
        public string? ENVC3H8 { get; set; }
        [ExcelColumn("i-C4H10")]
        public string? ENViC4H10 { get; set; }
        [ExcelColumn("n-C4H10")]
        public string? ENVnC4H10 { get; set; }
        [ExcelColumn("i-C5H12")]
        public string? ENViC5H12 { get; set; }
        [ExcelColumn("n-C5H12")]
        public string? ENVnC5H12 { get; set; }
        [ExcelColumn("C6+")]
        public string? ENVC6 { get; set; }
        [ExcelColumn("H2")]
        public string? ENVH2 { get; set; }
        [ExcelColumn("流量计口径")]
        public string? EquipmentCaliber { get; set; }
        [ExcelColumn("流量测量范围(m3/h)")]
        public string? EquipmentMeasurementRange { get; set; }
        [ExcelColumn("最大工作压力(mPa)")]
        public string? EquipmnetMaxWorkPressure { get; set; }
        [ExcelColumn("K系数")]
        public string? EquipmentKFactor { get; set; }
        [ExcelColumn("单点修正系数")]
        public string? EquipmentCorrectionCoefficient { get; set; }
        [ExcelColumn("压力等级")]
        public string? EquipmentPressureClass { get; set; }
        [ExcelColumn("qt以上最大示值误差(%)")]
        public string? RSQtUpMaxIndicatingValueError { get; set; }
        [ExcelColumn("qt以下最大示值误差(%)")]
        public string? RSQtDNMaxIndicatingValueError { get; set; }
        [ExcelColumn("qt以上重复性(%)")]
        public string? RSQtUpRepeatability { get; set; }
        [ExcelColumn("qt以下重复性(%)")]
        public string? RSQtDNRepeatability { get; set; }
        [ExcelColumn("外观技术要求")]
        public string? RSAppearanceTechicalRequirement { get; set; }
        [ExcelColumn("外观检定结果")]
        public string? RSAppearanceVerifiedResult { get; set; }
        [ExcelColumn("绝缘电阻技术要求")]
        public string? RSInsulationResistanceTechicalRequirement { get; set; }
        [ExcelColumn("绝缘电阻检定结果")]
        public string? RSInsulationResistanceVerifiedResult { get; set; }
        [ExcelColumn("示值误差技术要求")]
        public string? RsIndicatingValueErrorTechicalRequirement { get; set; }
        [ExcelColumn("示值误差检定结果")]
        public string? RsIndicatingValueErrorVerifiedResult { get; set; }
        [ExcelColumn("回差技术要求")]
        public string? RSReturnDifferenceTechicalRequirement { get; set; }
        [ExcelColumn("回差检定结果")]
        public string? RSReturnDifferenceVerifiedResult { get; set; }
        public string? RSAppearance { get; set; }
        public string? RSInsulationResistance { get; set; }
        [ExcelColumn("备注")]
        public string? Note { get; set; }
        [NotMapped]
        public List<EquipmentMeteringCheckedData>? MeteringCheckedDatas { get; set; }
        //[NotMapped]
        //public List<EquipmentMeteringDetectingEquipment>? MeteringDetectingEquipment { get; set; }
        [NotMapped]
        public List<EquipmentMeteringResultData>? MeteringResultDatas { get; set; }
    }
}
