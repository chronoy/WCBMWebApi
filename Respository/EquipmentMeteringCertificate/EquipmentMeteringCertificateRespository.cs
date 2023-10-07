using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class EquipmentMeteringCertificateRespository : IEquipmentMeteringCertificateRespository
    {
        private readonly SQLServerDBContext _context;
        public EquipmentMeteringCertificateRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<EquipmentMeteringCertificate> GetEquipmentMeteringCertificates()
        {
            List<EquipmentMeteringCertificate> equipmentMeteringCertificates = new();
            equipmentMeteringCertificates = (from certificate in _context.EquipmentMeteringCertificates
                                             select certificate).ToList();
            equipmentMeteringCertificates.ForEach(certificate =>
            {
                certificate.MeteringCheckedDatas = (from check in _context.EquipmentMeteringCheckedDatas
                                                    where check.MeteringCertificateID == certificate.ID
                                                    select check).ToList();
                certificate.MeteringDetectingEquipment = (from detect in _context.EquipmentMeteringDetectingEquipment
                                                          where detect.MeteringCertificateID == certificate.ID
                                                          select detect).ToList();
                certificate.MeteringResultDatas = (from result in _context.EquipmentMeteringResultDatas
                                                   where result.MeteringCertificateID == certificate.ID
                                                   select result).ToList();
            });
            return equipmentMeteringCertificates;
        }
    }
}
