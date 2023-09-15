using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Keyless]
    public class AlarmCount
    {
        public string Name { get; set; }
        public string AlarmName { get; set; }
        public string AlarmArea { get; set; }
        public int Count { get; set; }
    }
}
