﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Respository
{
    public  class StationLoopRespository:IStationLoopRespository
    {
        private readonly SQLServerDBContext _context;
        public StationLoopRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<StationLoop> GetStationLoops()
        {
            return _context.StationLoops.ToList();  
        }
    }
}