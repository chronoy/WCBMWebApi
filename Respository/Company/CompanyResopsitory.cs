using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Respository
{
    public  class CompanyResopsitory:ICompanyRespository
    {
        private readonly SQLServerDBContext _context;
        public CompanyResopsitory(SQLServerDBContext context)
        {
            _context = context;
        }

        public List<Company> GetCompanies()
        {
            return _context.companies.ToList();
        }
    }
}
