using Models;
using System.Linq.Expressions;

namespace Respository
{
    public interface IReferenceMaterialCertificateRespository
    {
        public List<ReferenceMaterialCertificate> GetReferenceMaterialCertificates(DateTime beginSearchDate, DateTime endSearchDate);
        public string AddReferenceMaterialCertificate(ReferenceMaterialCertificate entity);
        public string AddReferenceMaterialCertificates(List<ReferenceMaterialCertificate> entities);
        public string UpdateReferenceMaterialCertificate(ReferenceMaterialCertificate entity);
        public bool UpdateReferenceMaterialCertificates(List<ReferenceMaterialCertificate> listEntity);
        public bool DeleteReferenceMaterialCertificateBy(Expression<Func<ReferenceMaterialCertificate, bool>> whereLambda);
    }
}