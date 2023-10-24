using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class ReferenceMaterialCertificateRespository : IReferenceMaterialCertificateRespository
    {
        private readonly SQLServerDBContext _context;
        public ReferenceMaterialCertificateRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<ReferenceMaterialCertificate> GetReferenceMaterialCertificates(DateTime beginSearchDate, DateTime endSearchDate)
        {
            IEnumerable<ReferenceMaterialCertificate> result = _context.ReferenceMaterialCertificates.Where(x => x.CertificationDate >= beginSearchDate && x.CertificationDate <= endSearchDate);

            return result.ToList();
        }

        public string AddReferenceMaterialCertificate(ReferenceMaterialCertificate entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = DateTime.Now;
                _context.ReferenceMaterialCertificates.Add(entity);
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

        public string AddReferenceMaterialCertificates(List<ReferenceMaterialCertificate> entities)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                entities.ForEach(e => { e.CreatedDate = DateTime.Now; e.UpdatedDate = DateTime.Now; });
                _context.ReferenceMaterialCertificates.AddRange(entities);
                _context.SaveChanges();
                entities.ForEach(e => _context.Entry(e));
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                return "OtherError";
            }
            return "OK";
        }

        public string UpdateReferenceMaterialCertificate(ReferenceMaterialCertificate entity)
        {
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                var certificate = _context.ReferenceMaterialCertificates.FirstOrDefault(x => x.ID == entity.ID);
                if (certificate != null)
                {
                    certificate.UpdatedDate = DateTime.Now;
                    certificate.ReferenceMaterialNumber = entity.ReferenceMaterialNumber;
                    certificate.BatchNumber = entity.BatchNumber;
                    certificate.CertificationDate = entity.CertificationDate;
                    certificate.ValidityPeriod = entity.ValidityPeriod;
                    certificate.ReferenceMaterialProdoucer = entity.ReferenceMaterialProdoucer;
                    certificate.Address = entity.Address;
                    certificate.Telephone = entity.Telephone;
                    certificate.Email = entity.Email;
                    certificate.Version = entity.Version;
                    certificate.SampleNumber = entity.SampleNumber;
                    certificate.Hexane = entity.Hexane;
                    certificate.Pentane = entity.Pentane;
                    certificate.Isopentane = entity.Isopentane;
                    certificate.Neopentane = entity.Neopentane;
                    certificate.Butane = entity.Butane;
                    certificate.Isobutane = entity.Isobutane;
                    certificate.Propane = entity.Propane;
                    certificate.Ethane = entity.Ethane;
                    certificate.CarbonDioxide = entity.CarbonDioxide;
                    certificate.Nitrogen = entity.Nitrogen;
                    certificate.Methane = entity.Methane;
                    certificate.Uncertainty = entity.Uncertainty;
                    _context.ReferenceMaterialCertificates.Update(certificate);
                    _context.SaveChanges();
                    tran.Commit();
                }
            }
            catch (Exception)
            {
                tran.Rollback();
                return "OtherError";
            }
            return "OK";
        }

        public bool UpdateReferenceMaterialCertificates(List<ReferenceMaterialCertificate> listEntity)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                listEntity.ForEach(e => 
                {
                    var certificate = _context.ReferenceMaterialCertificates.FirstOrDefault(x => x.ID == e.ID);
                    if (certificate != null)
                    {
                        certificate.UpdatedDate = DateTime.Now;
                        certificate.ReferenceMaterialNumber = e.ReferenceMaterialNumber;
                        certificate.BatchNumber = e.BatchNumber;
                        certificate.CertificationDate = e.CertificationDate;
                        certificate.ValidityPeriod = e.ValidityPeriod;
                        certificate.ReferenceMaterialProdoucer = e.ReferenceMaterialProdoucer;
                        certificate.Address = e.Address;
                        certificate.Telephone = e.Telephone;
                        certificate.Email = e.Email;
                        certificate.Version = e.Version;
                        certificate.SampleNumber = e.SampleNumber;
                        certificate.Hexane = e.Hexane;
                        certificate.Pentane = e.Pentane;
                        certificate.Isopentane = e.Isopentane;
                        certificate.Neopentane = e.Neopentane;
                        certificate.Butane = e.Butane;
                        certificate.Isobutane = e.Isobutane;
                        certificate.Propane = e.Propane;
                        certificate.Ethane = e.Ethane;
                        certificate.CarbonDioxide = e.CarbonDioxide;
                        certificate.Nitrogen = e.Nitrogen;
                        certificate.Methane = e.Methane;
                        certificate.Uncertainty = e.Uncertainty;
                        _context.ReferenceMaterialCertificates.UpdateRange(certificate);
                    }
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

        public bool DeleteReferenceMaterialCertificateBy(Expression<Func<ReferenceMaterialCertificate, bool>> whereLambda)
        {
            bool result = false;
            using var tran = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<ReferenceMaterialCertificate> listDeleting = _context.ReferenceMaterialCertificates.Where(whereLambda).ToList();
                listDeleting.ForEach(u =>
                {
                    _context.ReferenceMaterialCertificates.Attach(u);
                    _context.ReferenceMaterialCertificates.Remove(u);
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
