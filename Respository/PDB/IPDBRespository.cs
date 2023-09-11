using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public interface IPDBRespository
    {
        public List<PDBTag> GetPDBTag();
    }
}
