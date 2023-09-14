using Models;
using Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductionReportService: IProductionReportService
    {
        public readonly IProductionReportRespository _productionReportRespository;

        public ProductionReportService(IProductionReportRespository productionReportRespository)
        {
            _productionReportRespository = productionReportRespository;
        }

        public Task<List<ProductionReport>> GetProductionReportData(string loopID, DateTime startDateTime, DateTime endDateTime)
        {
            return Task.Run(() => _productionReportRespository.GetProductionReportData(loopID, startDateTime, endDateTime));
        }
    }
}
