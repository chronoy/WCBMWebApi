﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface IStationEquipmentRespository
    {
        public List<StationEquipment> GetStationEquipments();
        public List<StationEquipment> GetStationEquipmentsBySttaion(int stationID);
        public StationEquipment GetStationEquipmentByID(int ID);
    }
}
