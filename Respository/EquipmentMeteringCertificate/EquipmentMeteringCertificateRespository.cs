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

        public List<EquipmentMeteringCertificate> GetEquipmentMeteringCertificates(DateTime beginSearchDate, DateTime endSearchDate)
        {
            List<EquipmentMeteringCertificate> equipmentMeteringCertificates = new();
            equipmentMeteringCertificates = (from certificate in _context.EquipmentMeteringCertificates
                                             where certificate.VerificationDate >= beginSearchDate && certificate.VerificationDate <= endSearchDate
                                             select certificate).ToList();
            equipmentMeteringCertificates.ForEach(certificate =>
            {
                certificate.MeteringCheckedDatas = (from check in _context.EquipmentMeteringCheckedDatas
                                                    where check.MeteringCertificateID == certificate.ID
                                                    select new EquipmentMeteringCheckedData
                                                    {
                                                        ID = check.ID,
                                                        MeteringCertificateID = check.MeteringCertificateID,
                                                        CertificateNumber = certificate.CertificateNumber,
                                                        V1 = check.V1,
                                                        V2 = check.V2,
                                                        V3 = check.V3
                                                    }).ToList();
                //certificate.MeteringDetectingEquipment = (from detect in _context.EquipmentMeteringDetectingEquipment
                //                                          where detect.MeteringCertificateID == certificate.ID
                //                                          select detect).ToList();
                certificate.MeteringResultDatas = (from result in _context.EquipmentMeteringResultDatas
                                                   where result.MeteringCertificateID == certificate.ID
                                                   select new EquipmentMeteringResultData
                                                   {
                                                       ID = result.ID,
                                                       MeteringCertificateID = result.MeteringCertificateID,
                                                       CertificateNumber = result.CertificateNumber,
                                                       V1 = result.V1,
                                                       V2 = result.V2,
                                                       V3 = result.V3,
                                                       V4 = result.V4,
                                                       V5 = result.V5,
                                                       V6 = result.V6
                                                   }).ToList();
            });
            return equipmentMeteringCertificates;
        }

        public string AddEquipmentMeteringCertificate(EquipmentMeteringCertificate entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                var equipment = _context.Equipments.FirstOrDefault(x => x.SerialNumber == entity.EquipmentSerialNumber);
                if (equipment != null)
                {
                    equipment.VerificationEndDate = entity.VerificationDate;
                    DateTime time = DateTime.MinValue + (entity.ValidityDate - entity.VerificationDate);
                    equipment.VerificationPeriod = (time.Year - 1) * 12 + time.Month - 1;
                    equipment.VerificationAgency = entity.Agency;
                    equipment.VerificationCertificateNumber = entity.CertificateNumber;
                    _context.Equipments.Update(equipment);
                    AddEntity(entity);
                    if (entity.MeteringCheckedDatas != null)
                    {
                        entity.MeteringCheckedDatas.ForEach(x => x.MeteringCertificateID = entity.ID);
                        _context.EquipmentMeteringCheckedDatas.AddRange(entity.MeteringCheckedDatas);
                    }
                    //if (entity.MeteringDetectingEquipment != null)
                    //{
                    //    _context.EquipmentMeteringDetectingEquipment.AddRange(entity.MeteringDetectingEquipment);
                    //}
                    if (entity.MeteringResultDatas != null)
                    {
                        entity.MeteringResultDatas.ForEach(x => x.MeteringCertificateID = entity.ID);
                        _context.EquipmentMeteringResultDatas.AddRange(entity.MeteringResultDatas);
                    }

                    _context.SaveChanges();
                    tran.Commit();
                }
                else
                {
                    return "NotExistEquipmentSerialNumber";
                }
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
                var equipment = _context.Equipments.FirstOrDefault(x => x.SerialNumber == entity.EquipmentSerialNumber);
                if (equipment != null)
                {
                    equipment.VerificationEndDate = entity.VerificationDate;
                    DateTime time = DateTime.MinValue + (entity.ValidityDate - entity.VerificationDate);
                    equipment.VerificationPeriod = (time.Year - 1) * 12 + time.Month - 1;
                    equipment.VerificationAgency = entity.Agency;
                    equipment.VerificationCertificateNumber = entity.CertificateNumber;
                    UpdateEntity(equipment);
                    UpdateEntity(entity);
                    if (entity.MeteringCheckedDatas != null)
                    {
                        UpdateEquipmentMeteringCheckedData(entity.MeteringCheckedDatas);
                    }
                    //if (entity.MeteringDetectingEquipment != null)
                    //{
                    //    UpdateEquipmentMeteringDetectingEquipment(entity.MeteringDetectingEquipment);
                    //}
                    if (entity.MeteringResultDatas != null)
                    {
                        UpdateEquipmentMeteringResultData(entity.MeteringResultDatas);
                    }
                }
                else
                {
                    return "NotExistEquipmentSerialNumber";
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

        //public string UpdateEquipmentMeteringDetectingEquipment(List<EquipmentMeteringDetectingEquipment> entity)
        //{
        //    return UpdateEntitys(entity);
        //}

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

                    //List<EquipmentMeteringDetectingEquipment> detectingEquipment = _context.EquipmentMeteringDetectingEquipment.Where(x => x.MeteringCertificateID == u.ID).ToList();
                    //detectingEquipment.ForEach(d =>
                    //{
                    //    _context.EquipmentMeteringDetectingEquipment.Attach(d);
                    //    _context.EquipmentMeteringDetectingEquipment.Remove(d);
                    //});

                    List<EquipmentMeteringResultData> resultDatas = _context.EquipmentMeteringResultDatas.Where(x => x.MeteringCertificateID == u.ID).ToList();
                    resultDatas.ForEach(r =>
                    {
                        _context.EquipmentMeteringResultDatas.Attach(r);
                        _context.EquipmentMeteringResultDatas.Remove(r);
                    });

                    var equipment = _context.Equipments.FirstOrDefault(x => x.SerialNumber == u.EquipmentSerialNumber);
                    if (equipment != null)
                    {
                        equipment.VerificationEndDate = null;
                        equipment.VerificationPeriod = null;
                        equipment.VerificationAgency = string.Empty;
                        equipment.VerificationCertificateNumber = string.Empty;
                        _context.Equipments.Update(equipment);
                    }

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
