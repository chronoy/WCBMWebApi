using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class CheckRespository : ICheckRespository
    {
        private readonly SQLServerDBContext _context;
        public CheckRespository(SQLServerDBContext context)
        {
            _context = context;
        }
        public List<StationEquipmentCheckData> GetStationEquipmentCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime)
        {
            //ABB
            List<StationEquipmentCheckData> abbGC=_context.ABBGCCheckDatas.ToList<StationEquipmentCheckData>();
            //Daniel
            List<StationEquipmentCheckData> danielGC = _context.DanielGCCheckDatas.ToList<StationEquipmentCheckData>();
            //Elster
            List<StationEquipmentCheckData> elsterGC = _context.DanielGCCheckDatas.ToList<StationEquipmentCheckData>();
            return null;
        }

        public List<StationLoopCheckData> GetStationLoopCheckReport(string reportCategory, int equipmentID, string startDateTime, string endDateTime)
        {
            //Daniel
            List<StationLoopCheckData> danielVOS= _context.DanielVOSCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> danielFR = _context.DanielFRCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> danielLoop = _context.DanielLoopCheckDatas.ToList<StationLoopCheckData>();
            //Elster
            List<StationLoopCheckData> elsterVOS = _context.ElsterVOSCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> elsterFR = _context.ElsterFRCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> elsterLoop = _context.ElsterLoopCheckDatas.ToList<StationLoopCheckData>();
            //Sick
            List<StationLoopCheckData> sickVOS = _context.SickVOSCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> sickFR = _context.SickFRCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> sickLoop = _context.SickLoopCheckDatas.ToList<StationLoopCheckData>();
            //Weise
            List<StationLoopCheckData> weiseVOS = _context.WeiseVOSCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> weiseFR = _context.WeiseFRCheckDatas.ToList<StationLoopCheckData>();
            List<StationLoopCheckData> weiseLoop = _context.WeiseLoopCheckDatas.ToList<StationLoopCheckData>();
            return null;

        }
    }
}
