using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class EquipmentMeteringCertificateRespository : BaseClass, IEquipmentMeteringCertificateRespository
    {
        private readonly SQLServerDBContext _context;
        public EquipmentMeteringCertificateRespository(SQLServerDBContext context) : base(context)
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

        public string AddEquipmentMeteringCertificate(EquipmentMeteringCertificate entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.EquipmentMeteringCertificates.Add(entity);
                var equipment = _context.Equipments.FirstOrDefault(x => x.SerialNumber == entity.EquipmentSerialNumber);
                if (equipment != null)
                {
                    equipment.VerificationEndDate = entity.VerificationDate;
                    DateTime time = DateTime.MinValue + (entity.ValidityDate - entity.VerificationDate);
                    equipment.VerificationPeriod = (time.Year - 1) * 12 + time.Month - 1;
                    equipment.VerificationAgency = entity.Agency;
                    equipment.VerificationCertificateNumber = entity.CertificateNumber;
                    _context.Equipments.Update(equipment);
                    if (entity.MeteringCheckedDatas != null)
                    {
                        _context.EquipmentMeteringCheckedDatas.AddRange(entity.MeteringCheckedDatas);
                    }
                    if (entity.MeteringDetectingEquipment != null)
                    {
                        _context.EquipmentMeteringDetectingEquipment.AddRange(entity.MeteringDetectingEquipment);
                    }
                    if (entity.MeteringResultDatas != null)
                    {
                        _context.EquipmentMeteringResultDatas.AddRange(entity.MeteringResultDatas);
                    }
                }
                _context.SaveChanges();
                _context.Entry(entity);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string UpdateEquipmentMeteringCertificate(EquipmentMeteringCertificate entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                _context.EquipmentMeteringCertificates.Update(entity);
                var equipment = _context.Equipments.FirstOrDefault(x => x.SerialNumber == entity.EquipmentSerialNumber);
                if (equipment != null)
                {
                    equipment.VerificationEndDate = entity.VerificationDate;
                    DateTime time = DateTime.MinValue + (entity.ValidityDate - entity.VerificationDate);
                    equipment.VerificationPeriod = (time.Year - 1) * 12 + time.Month - 1;
                    equipment.VerificationAgency = entity.Agency;
                    equipment.VerificationCertificateNumber = entity.CertificateNumber;
                    _context.Equipments.Update(equipment);
                    if (entity.MeteringCheckedDatas != null)
                    {
                        UpdateEquipmentMeteringCheckedData(entity.MeteringCheckedDatas);
                    }
                    if (entity.MeteringDetectingEquipment != null)
                    {
                        UpdateEquipmentMeteringDetectingEquipment(entity.MeteringDetectingEquipment);
                    }
                    if (entity.MeteringResultDatas != null)
                    {
                        UpdateEquipmentMeteringResultData(entity.MeteringResultDatas);
                    }
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string UpdateEquipmentMeteringCheckedData(List<EquipmentMeteringCheckedData> entity)
        {
            return UpdateEntitys(entity);
        }

        public string UpdateEquipmentMeteringDetectingEquipment(List<EquipmentMeteringDetectingEquipment> entity)
        {
            return UpdateEntitys(entity);
        }

        public string UpdateEquipmentMeteringResultData(List<EquipmentMeteringResultData> entity)
        {
            return UpdateEntitys(entity);
        }

        public bool DeleteEquipmentMeteringCertificate(List<int> ids)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<EquipmentMeteringCertificate> listDeleting = _context.EquipmentMeteringCertificates.Where(x => ids.Contains(x.ID)).ToList();
                listDeleting.ForEach(u =>
                {
                    List<EquipmentMeteringCheckedData> checkedDatas = _context.EquipmentMeteringCheckedDatas.Where(x => x.MeteringCertificateID == u.ID).ToList();
                    checkedDatas.ForEach(c =>
                    {
                        _context.EquipmentMeteringCheckedDatas.Attach(c);
                        _context.EquipmentMeteringCheckedDatas.Remove(c);
                    });

                    List<EquipmentMeteringDetectingEquipment> detectingEquipment = _context.EquipmentMeteringDetectingEquipment.Where(x => x.MeteringCertificateID == u.ID).ToList();
                    detectingEquipment.ForEach(d =>
                    {
                        _context.EquipmentMeteringDetectingEquipment.Attach(d);
                        _context.EquipmentMeteringDetectingEquipment.Remove(d);
                    });

                    List<EquipmentMeteringResultData> resultDatas = _context.EquipmentMeteringResultDatas.Where(x => x.MeteringCertificateID == u.ID).ToList();
                    resultDatas.ForEach(r =>
                    {
                        _context.EquipmentMeteringResultDatas.Attach(r);
                        _context.EquipmentMeteringResultDatas.Remove(r);
                    });

                    _context.EquipmentMeteringCertificates.Attach(u);
                    _context.EquipmentMeteringCertificates.Remove(u);
                });
                result = _context.SaveChanges() > 0;
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                return result;
            }
            return result;
        }
    }
}
