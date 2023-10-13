using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EquipmentMeteringCertificateService: IEquipmentMeteringCertificateService
    {
        public readonly IEquipmentMeteringCertificateRespository _equipmentMeteringCertificateRespository;
        public EquipmentMeteringCertificateService(IEquipmentMeteringCertificateRespository equipmentMeteringCertificateRespository)
        {
            _equipmentMeteringCertificateRespository = equipmentMeteringCertificateRespository;
        }

        public Task<List<EquipmentMeteringCertificate>> GetEquipmentMeteringCertificates()
        {
            return Task.Run(()=>_equipmentMeteringCertificateRespository.GetEquipmentMeteringCertificates());
        }

        public Task<string> AddEquipmentMeteringCertificate(EquipmentMeteringCertificate entity)
        {
            return Task.Run(() => _equipmentMeteringCertificateRespository.AddEquipmentMeteringCertificate(entity));
        }

        public Task<string> UpdateEquipmentMeteringCertificate(EquipmentMeteringCertificate entity)
        {
            return Task.Run(() => _equipmentMeteringCertificateRespository.UpdateEquipmentMeteringCertificate(entity));
        }

        public Task<bool> DeleteEquipmentMeteringCertificate(List<int> ids)
        {
            return Task.Run(() => _equipmentMeteringCertificateRespository.DeleteEquipmentMeteringCertificate(ids));
        }
    }
}
