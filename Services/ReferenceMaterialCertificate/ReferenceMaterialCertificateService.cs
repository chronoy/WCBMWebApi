using iText.StyledXmlParser.Jsoup.Nodes;
using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static iText.IO.Util.IntHashtable;

namespace Services
{
    public class ReferenceMaterialCertificateService : IReferenceMaterialCertificateService
    {
        public readonly IReferenceMaterialCertificateRespository _referenceMaterialCertificateRespository;
        public ReferenceMaterialCertificateService(IReferenceMaterialCertificateRespository referenceMaterialCertificateRespository)
        {
            _referenceMaterialCertificateRespository = referenceMaterialCertificateRespository;
        }

        public Task<List<ReferenceMaterialCertificate>> GetReferenceMaterialCertificates(DateTime beginSearchDate, DateTime endSearchDate)
        {
            return Task.Run(() => _referenceMaterialCertificateRespository.GetReferenceMaterialCertificates(beginSearchDate, endSearchDate));
        }

        public Task<string> AddReferenceMaterialCertificate(ReferenceMaterialCertificate entity)
        {
            return Task.Run(() => _referenceMaterialCertificateRespository.AddReferenceMaterialCertificate(entity));
        }
        public Task<string> AddReferenceMaterialCertificates(List<ReferenceMaterialCertificate> entities)
        {
            return Task.Run(() => _referenceMaterialCertificateRespository.AddReferenceMaterialCertificates(entities));
        }

        public Task<string> UpdateReferenceMaterialCertificate(ReferenceMaterialCertificate entity)
        {
            return Task.Run(() => _referenceMaterialCertificateRespository.UpdateReferenceMaterialCertificate(entity));
        }
        public Task<bool> UpdateReferenceMaterialCertificates(List<ReferenceMaterialCertificate> listEntity)
        {
            return Task.Run(() => _referenceMaterialCertificateRespository.UpdateReferenceMaterialCertificates(listEntity));
        }

        public Task<bool> DeleteReferenceMaterialCertificateBy(Expression<Func<ReferenceMaterialCertificate, bool>> whereLambda)
        {
            return Task.Run(() => _referenceMaterialCertificateRespository.DeleteReferenceMaterialCertificateBy(whereLambda));
        }
    }
}
