using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public class AreaRespository: IAreaRespository
    {
        private readonly SQLServerDBContext _context;
        public AreaRespository(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<Area> GetAreas()
        {
           return _context.Areas.ToList();  
        }
    }
}
