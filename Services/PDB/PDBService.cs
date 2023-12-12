using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using Respository;
using System.Linq;
using System.Timers;
using System.ComponentModel;
using static System.Collections.Specialized.BitVector32;

namespace Services
{
    public class PDBService : IPDBService
    {
        public List<PDBTag> _tags = new List<PDBTag>();

        private readonly IPDBRespository _PDBRespository;
        private readonly IOPCClientService _OPCClientService;
        private System.Timers.Timer _tmrUpdate;
        //public PDBService(IConfiguration configuration, IOPCClientService OPCClientService)
        //{
        //    //_PDBRespository = new PDBResposiory();
        //    _OPCClientService = OPCClientService;
        //    //tags = _PDBRespository.GetTags(configuration["IFIXNodeName"]).ToList<Tag>();
        //}

        public PDBService(IOPCClientService OPCClientService)
        {
            _PDBRespository = new PDBRespository();
            _tags=_PDBRespository.GetPDBTag();
            _OPCClientService = OPCClientService;
        }
        public List<PDBTag> GetAllPDBTags()
        {
            return _tags;
        }

        private void Update_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int i;
            List<OpcItem> OPCItems = _OPCClientService.GetAllOPCItems();
            for (i = 0; i < OPCItems.Count; i++)
            {
                _tags[i].Value = OPCItems[i].Value;
                _tags[i].Quality = OPCItems[i].Quality;
            }
        }

        //public List<Tag> GetAllPDBTags()
        //{
        //    //if(_tags.Count==0)
        //    //{
        //    //    WriteLog("OPC New");
        //    //}
        //    return _tags;
        //}


        public Task<List<PDBTag>> GetLoopTagsByStation(Station station)
        {
            return Task.Run(() =>
            {
                List<PDBTag> stationTags = _tags.FindAll(tag => tag.Name.Split('_')[0] == station.AbbrName);
                stationTags.ForEach(x => x.GetStatus());
                return stationTags;
            });
        }

        public Task<List<PDBTag>> GetGCComponent(StationEquipment equipment)
        {
            return Task.Run(() => {
                List<string> componentNames=new List<string>() { equipment.StationAbbrName + "_" + equipment.AbbrName + "_C1" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_C2" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_C3" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_NC4" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_IC4" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_NC5" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_IC5" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_NeoC5" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_C6" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_N2" ,
                                                             equipment.StationAbbrName + "_" + equipment.AbbrName + "_CO2"
                                                            };
                List<PDBTag> tags = _tags.Where(tag => componentNames.Contains(tag.Name)).ToList();
                return tags;
            });
        }

        //public Task<Dictionary<string, Tag>> GetPDBTags(List<string> TagNames)
        //{
        //    return Task.Run(() =>
        //    {
        //        Dictionary<string, Tag> tags = new Dictionary<string, Tag>();
        //        Dictionary<string, Tag> allTags = _tags.ToDictionary(key => key.Name, value => value);
        //        foreach (string key in TagNames)
        //        {
        //            tags[key] = allTags[key];
        //        }
        //        return tags;
        //    });
        //}
        public void Run()
        {
            _tmrUpdate = new System.Timers.Timer(5000);
            //Globe.writeLog("Watch Dog Start");
            _tmrUpdate.Elapsed += new ElapsedEventHandler(Update_Elapsed);
            _tmrUpdate.Enabled = true;
            _tmrUpdate.AutoReset = true;
            Update_Elapsed(null, null);
        }
       
    }
}


