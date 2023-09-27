using Models;
using Respository;
using System.Linq.Expressions;

namespace Services
{
    public interface IReferenceMaterialCertificateService
    {
        public Task<List<ReferenceMaterialCertificate>> GetReferenceMaterialCertificates(DateTime beginSearchDate, DateTime endSearchDate);
        public Task<string> AddReferenceMaterialCertificate(ReferenceMaterialCertificate entity);
        public Task<string> AddReferenceMaterialCertificates(List<ReferenceMaterialCertificate> entities);
        public Task<string> UpdateReferenceMaterialCertificate(ReferenceMaterialCertificate entity);
        public Task<bool> UpdateReferenceMaterialCertificates(List<ReferenceMaterialCertificate> listEntity);
        public Task<bool> DeleteReferenceMaterialCertificateBy(Expression<Func<ReferenceMaterialCertificate, bool>> whereLambda);
    }
}