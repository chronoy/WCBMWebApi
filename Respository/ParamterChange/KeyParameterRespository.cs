using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class KeyParameterRespository : BaseClass, IKeyParameterRespository
    {
        private readonly SQLServerDBContext _context;
        public KeyParameterRespository(SQLServerDBContext context) : base(context)
        {
            _context = context;
        }
        public List<KeyParameter> GetKeyParametersByLoop(List<int> loopIDs)
        {
            return GetEntitys<KeyParameter>(para => loopIDs.Contains(para.LoopID));
        }

        public List<KeyParametersChangeRecord> GetKeyParametersChangeRecordByLoop(DateTime beginTime, DateTime endTime, List<int> loopIDs)
        {
            return (from record in
                   (from record in _context.KeyParametersChangeRecords
                    where DateTime.Compare(record.DateTime, beginTime) >= 0 && DateTime.Compare(record.DateTime, endTime) <= 0
                    select record)
                    join key in _context.KeyParameters.Where(key => loopIDs.Contains(key.LoopID))
                    on record.KeyParameterID equals key.ID
                    select new KeyParametersChangeRecord
                    {
                        ID = record.ID,
                        DateTime = record.DateTime,
                        KeyParameterID = record.KeyParameterID,
                        LastValue = record.LastValue,
                        CurrentValue = record.CurrentValue,
                        Operator = record.Operator,
                        Description = key.Description
                    }).OrderByDescending(obj => obj.DateTime).ToList();
            //return GetEntitys<KeyParametersChangeRecord>(record => loopIDs.Contains(record.KeyParameterID/100) &&
            //                                                     DateTime.Compare(record.DateTime, beginTime) >= 0 && DateTime.Compare(record.DateTime, endTime) <= 0);
        }
    }
}
