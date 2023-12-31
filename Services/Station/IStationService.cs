﻿using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public interface IStationService
    {
        public Task<List<Station>> GetStations();
        public Task<Station> GetStationByID(int ID);
        public Task<List<Station>> GetStationsByStations(List<int> stationIDs);
        public Task<List<Station>> GetStationsByCompanyLine(List<int> companyIDs, List<int> lineIDs);
    }
}
