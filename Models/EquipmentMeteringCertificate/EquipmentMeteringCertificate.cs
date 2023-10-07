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
        public string Agency { get; set; }
        public string CertificateNumber { get; set; }
        public string? Customer { get; set; }
        public string? CustomerContact { get; set; }
        public string? CustomerAddress { get; set; }
        public string? EquipmentCategory { get; set; }
        public string? EquipmentManufacturer { get; set; }
        public string? EquipmentModel { get; set; }
        public string? EquipmentAccuracy { get; set; }
        public string? CertificateConclusion { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ApproverPosition { get; set; }
        public string? CheckedBy { get; set; }
        public string? VerifiedBy { get; set; }
        public string? CelibratedBy { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime VerificationDate { get; set; }
        public DateTime ValidityDate { get; set; }
        public DateTime AuthorizationDate { get; set; }
        public DateTime CelibrationDate { get; set; }
        public DateTime IssueDate { get; set; }
        public string? ReferenceRegulation { get; set; }
        public string? ENVMedium { get; set; }
        public string? ENVFlowrate { get; set; }
        public string? ENVMediumTemperature { get; set; }
        public string? ENVEnvironmentTemperature { get; set; }
        public string? ENVMediumPerssure { get; set; }
        public string? ENVEnvironmentPressure { get; set; }
        public string? ENVMediumHumidity { get; set; }
        public string? ENVEnvironmentHumidity { get; set; }
        public string? ENVRelativeHumidity { get; set; }
        public string? ENVInstallationCondition { get; set; }
        public string? ENVInstallationDirection { get; set; }
        public string? ENVRestrictiveCondition { get; set; }
        public string? ENVRestrictiveMeasurementRange { get; set; }
        public string? ENVLocation { get; set; }
        public string? ENVN2 { get; set; }
        public string? ENVCH4 { get; set; }
        public string? ENVCO2 { get; set; }
        public string? ENVC2H6 { get; set; }
        public string? ENVC3H8 { get; set; }
        public string? ENViC4H10 { get; set; }
        public string? ENVnC4H10 { get; set; }
        public string? ENViC5H12 { get; set; }
        public string? ENVnC5H12 { get; set; }
        public string? ENVC6 { get; set; }
        public string? ENVH2 { get; set; }
        public string? EquipmentCaliber { get; set; }
        public string? EquipmentMeasurementRange { get; set; }
        public string? EquipmnetMaxWorkPressure { get; set; }
        public string? EquipmentKFactor { get; set; }
        public string? EquipmentCorrectionCoefficient { get; set; }
        public string? EquipmentPressureClass { get; set; }
        public string? RSQtUpMaxIndicatingValueError { get; set; }
        public string? RSQtDNMaxIndicatingValueError { get; set; }
        public string? RSQtUpRepeatability { get; set; }
        public string? RSQtDNRepeatability { get; set; }
        public string? RSAppearanceTechicalRequirement { get; set; }
        public string? RSAppearanceVerifiedResult { get; set; }
        public string? RSInsulationResistanceTechicalRequirement { get; set; }
        public string? RSInsulationResistanceVerifiedResult { get; set; }
        public string? RsIndicatingValueErrorTechicalRequirement { get; set; }
        public string? RsIndicatingValueErrorVerifiedResult { get; set; }
        public string? RSReturnDifferenceTechicalRequirement { get; set; }
        public string? RSReturnDifferenceVerifiedResult { get; set; }
        public string? RSAppearance { get; set; }
        public string? RSInsulationResistance { get; set; }
        public string? Note { get; set; }
        [NotMapped]
        public List<EquipmentMeteringCheckedData>? MeteringCheckedDatas { get; set; }
        [NotMapped]
        public List<EquipmentMeteringDetectingEquipment>? MeteringDetectingEquipment { get; set; }
        [NotMapped]
        public List<EquipmentMeteringResultData>? MeteringResultDatas { get; set; }
    }
}
