﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface IProductionReportRespository
    {
        public List<ProductionReport> GetProductionReportData(string loopID, DateTime startDateTime, DateTime endDateTime);
    }
}
