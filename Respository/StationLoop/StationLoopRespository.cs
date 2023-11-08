using System;
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

        public List<StationLoop> GetStationLoopsByStation(int stationID)
        {
            return  (from loop in _context.StationLoops.Where(obj => obj.StationID == stationID)
                    join category in _context.EquipmentCategories
                    on loop.EquipmentCategoryID equals  category.ID
                    select new StationLoop
                    {
                        ID = loop.ID,
                        Name = loop.Name,
                        AbbrName=loop.AbbrName,
                        CollectDataTypeID=loop.CollectDataTypeID,
                        EquipmentCategoryID=loop.EquipmentCategoryID,
                        StationID=loop.StationID,
                        LineID=loop.LineID,
                        Caliber=loop.Caliber,
                        Customer=loop.Customer,
                        FlowmeterManufacturer=loop.FlowmeterManufacturer,
                        FlowmeterModel=loop.FlowmeterModel,
                        FlowComputerManufacturer=loop.FlowmeterManufacturer,
                        FlowComputerModel = loop.FlowComputerModel,
                        OrderNumber=loop.OrderNumber,
                        EquipmentCategoryName=category.Name
                    }).ToList()
                    .OrderBy(obj => obj.OrderNumber).ToList();
        }
    }
}
