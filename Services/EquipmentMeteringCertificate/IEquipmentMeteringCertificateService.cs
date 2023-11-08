using Models;

namespace Services
{
    public interface IEquipmentMeteringCertificateService
    {
        public Task<List<EquipmentMeteringCertificate>> GetEquipmentMeteringCertificates(DateTime beginSearchDate, DateTime endSearchDate);
        public Task<string> AddEquipmentMeteringCertificate(EquipmentMeteringCertificate entity);
        public Task<string> UpdateEquipmentMeteringCertificate(EquipmentMeteringCertificate entity);
        public Task<bool> DeleteEquipmentMeteringCertificate(List<int> ids);
        public Task<byte[]> ExportEquipmentMeteringCertificates(List<EquipmentMeteringCertificate> equipmentMeteringCertificates, string templatePath);
    }
}