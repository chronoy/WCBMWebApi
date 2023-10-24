using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class StationRespository : IStationRespository
    {
        private readonly SQLServerDBContext _context;
        public StationRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public Station GetStationByID(int ID)
        {
            return _context.Stations.FirstOrDefault(obj => obj.ID == ID);
        }

        public List<Station> GetStations()
        {
            return _context.Stations.ToList();
        }

        public List<Station> GetStationsByStations(List<int> stationIDs)
        {
            return (from station in _context.Stations.Where(station => stationIDs.Contains(station.ID))
                    select new Station
                    {
                        ID = station.ID,
                        Name = station.Name,
                        AbbrName = station.AbbrName,
                        Phone = station.Phone,
                        Address = station.Address,
                        PostalCode = station.PostalCode,
                        Description = station.Description,
                        AreaID = station.AreaID,
                        CollectorID = station.CollectorID,
                        Loops =(from loop in _context.StationLoops.Where(loop=>loop.StationID==station.ID)
                                join collectdatatype in _context.StationDeviceCollectDataTypes
                                on loop.CollectDataTypeID equals collectdatatype.ID
                                join category in _context.EquipmentCategories
                                on loop.EquipmentCategoryID equals category.ID
                                select new StationLoop {
                                ID = loop.ID,
                                AbbrName= loop.AbbrName,
                                Name = loop.Name,
                                CollectDataTypeID = loop.CollectDataTypeID,
                                EquipmentCategoryID = loop.EquipmentCategoryID,
                                FlowComputerManufacturer=loop.FlowComputerManufacturer,
                                FlowComputerModel=loop.FlowComputerModel,
                                FlowmeterManufacturer=loop.FlowmeterManufacturer,
                                FlowmeterModel=loop.FlowmeterModel,
                                EquipmentCategoryName= category.Name.Contains("超声流量计")? collectdatatype.Manufacturer+"超声" : category.Name.Contains("质量流量计")? collectdatatype.Manufacturer + "质量": collectdatatype.Manufacturer + "涡轮"

                                }).ToList(),
                        Equipments=_context.StationEquipments.Where(equipment=> equipment.StationID==station.ID).ToList()
                    }).ToList();
        }
    }
}
