using Models;

namespace Respository
{
    public interface IEquipmentMeteringCertificateRespository
    {
        public List<EquipmentMeteringCertificate> GetEquipmentMeteringCertificates(DateTime beginSearchDate, DateTime endSearchDate);
        public string AddEquipmentMeteringCertificate(EquipmentMeteringCertificate entity);
        public string UpdateEquipmentMeteringCertificate(EquipmentMeteringCertificate entity);
        public string UpdateEquipmentMeteringCheckedData(List<EquipmentMeteringCheckedData> entity);
        //public string UpdateEquipmentMeteringDetectingEquipment(List<EquipmentMeteringDetectingEquipment> entity);
        public string UpdateEquipmentMeteringResultData(List<EquipmentMeteringResultData> entity);
        public bool DeleteEquipmentMeteringCertificate(List<int> ids);
    }
}