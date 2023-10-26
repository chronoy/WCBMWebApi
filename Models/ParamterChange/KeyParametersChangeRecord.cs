using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tKeyParametersChangeRecord")]
    public class KeyParametersChangeRecord
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public int KeyParameterID { get; set; }
        public double LastValue { get; set; }
        public double CurrentValue { get; set; }
        public string Operator { get; set; }
        [NotMapped]
        public string Description { get; set; }
    }
}
