using System;
using System.Collections.Generic;
using Models;
using System.Threading.Tasks;

namespace Services
{
    public interface IPDBService
    {
        public List<PDBTag> GetAllPDBTags();
        //  public Task<Dictionary<string, Dictionary<string, Dictionary<string, Tag>>>> GetPDBTagsByStation(Station station);
        //public Task<List<Tag>> GetPDBTagsByStation(Station station);

        //public Task<Dictionary<string, Tag>> GetPDBTags(List<string> TagNames);
        public void Run();
    }
}
