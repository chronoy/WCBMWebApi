﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tEquipmentCategory")]
    public class EquipmentCategory
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
