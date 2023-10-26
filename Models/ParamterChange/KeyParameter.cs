using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tKeyParameter")]
    public class KeyParameter
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public double LastValue { get; set; }
        public double CurrentValue { get; set; }
        public string Operator { get; set; }
        [Column("DescriptionCN")]
        public string Description { get; set; }
        public int LoopID { get; set; }
    }
}
