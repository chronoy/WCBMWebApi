using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UnnormalizedComponentsCheckData
    {
        public string DateTime { get; set; }
        public float? Value { get; set; }
        public string Condition { get;set; } 
        public string Result { get; set; }
    }
}
