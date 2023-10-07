using Models;

namespace Services
{
    public interface IEquipmentMeteringCertificateService
    {
        public Task<List<EquipmentMeteringCertificate>> GetEquipmentMeteringCertificates();
    }
}