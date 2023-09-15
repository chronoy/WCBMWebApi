using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tEquipmentStatus")]
    public class EquipmentStatus
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
