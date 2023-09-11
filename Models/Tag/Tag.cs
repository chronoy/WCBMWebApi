using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [Table("tTag")]
    public class Tag
    {
        [Key]
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public int StationDeviceCollectID { get; set; }
    }
    public class PDBTag
    {
        [Key]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Value { get; set; } = "????";
        public string Quality { get; set; } = "Uncertain";
    }
}
