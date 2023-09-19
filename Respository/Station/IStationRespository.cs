﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Respository
{
    public interface IStationRespository
    {
        public List<Station> GetStations();
        public Station GetStationByID(int ID);
    }
}
