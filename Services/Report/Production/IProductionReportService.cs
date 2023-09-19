using Models;

namespace Services
{
    public interface IProductionReportService
    {
        public Task<List<ProductionReport>> GetProductionReportData(string loopID, DateTime startDateTime, DateTime endDateTime);
    }
}