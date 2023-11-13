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
                        Loops = (from loop in _context.StationLoops.Where(loop => loop.StationID == station.ID)
                                 join collectdatatype in _context.StationDeviceCollectDataTypes
                                 on loop.CollectDataTypeID equals collectdatatype.ID
                                 join category in _context.EquipmentCategories
                                 on loop.EquipmentCategoryID equals category.ID
                                 select new StationLoop
                                 {
                                     ID = loop.ID,
                                     AbbrName = loop.AbbrName,
                                     Name = loop.Name,
                                     CollectDataTypeID = loop.CollectDataTypeID,
                                     EquipmentCategoryID = loop.EquipmentCategoryID,
                                     FlowComputerManufacturer = loop.FlowComputerManufacturer,
                                     FlowComputerModel = loop.FlowComputerModel,
                                     FlowmeterManufacturer = loop.FlowmeterManufacturer,
                                     FlowmeterModel = loop.FlowmeterModel,
                                     EquipmentCategoryName = category.Name

                                 }).ToList(),
                        Equipments = _context.StationEquipments.Where(equipment => equipment.StationID == station.ID).ToList()
                    }).ToList();
        }

        public List<Station> GetStationsByCompanyLine(List<int> companyIDs, List<int> lineIDs)
        {
            List<int> stationIDs = new List<int>();
            List<Station> stations = new List<Station>();
            var LoopstationIDs = _context.StationLoops.Where(loop => lineIDs.Contains(loop.LineID)).Select(loop => loop.StationID).Distinct();
            var gcstationIDs = _context.StationEquipments.Where(equipment => lineIDs.Contains(equipment.LineID)).Select(equipment => equipment.StationID).Distinct();
            stationIDs.AddRange(LoopstationIDs); stationIDs.AddRange(gcstationIDs);
            if (stationIDs != null)
            {
                stationIDs = stationIDs.Distinct().ToList();
                stations=(from station in _context.Stations.Where(station => stationIDs.Contains(station.ID))
                 join area in _context.Areas
                 on station.AreaID equals area.ID
                 join company in _context.companies.Where(company => companyIDs.Contains(company.ID))
                 on area.CompanyID equals company.ID
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
                     CompanyName = company.Name,
                     Loops = (from loop in _context.StationLoops.Where(loop => loop.StationID == station.ID && lineIDs.Contains(loop.LineID))
                              join collectdatatype in _context.StationDeviceCollectDataTypes
                              on loop.CollectDataTypeID equals collectdatatype.ID
                              join category in _context.EquipmentCategories
                              on loop.EquipmentCategoryID equals category.ID
                              select new StationLoop
                              {
                                  ID = loop.ID,
                                  AbbrName = loop.AbbrName,
                                  Name = loop.Name,
                                  CollectDataTypeID = loop.CollectDataTypeID,
                                  EquipmentCategoryID = loop.EquipmentCategoryID,
                                  FlowComputerManufacturer = loop.FlowComputerManufacturer,
                                  FlowComputerModel = loop.FlowComputerModel,
                                  FlowmeterManufacturer = loop.FlowmeterManufacturer,
                                  FlowmeterModel = loop.FlowmeterModel,
                              }).ToList(),
                     Equipments = _context.StationEquipments.Where(equipment => equipment.StationID == station.ID).ToList()
                 }).ToList();
            }
            return stations;
        }
    }
}
