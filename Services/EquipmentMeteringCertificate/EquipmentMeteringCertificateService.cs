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
    }
}
