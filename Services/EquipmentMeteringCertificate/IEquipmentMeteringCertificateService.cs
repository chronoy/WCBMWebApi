using Models;

namespace Services
{
    public interface IEquipmentMeteringCertificateService
    {
        public Task<List<EquipmentMeteringCertificate>> GetEquipmentMeteringCertificates();
        public Task<string> AddEquipmentMeteringCertificate(EquipmentMeteringCertificate entity);
        public Task<string> UpdateEquipmentMeteringCertificate(EquipmentMeteringCertificate entity);
        public Task<bool> DeleteEquipmentMeteringCertificate(List<int> ids);
    }
}