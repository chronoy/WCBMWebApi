using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Table("tUser")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PersonName { get; set; }
        public string ContactNumber { get; set; }
        public string RoleName { get; set; }
        [NotMapped]
        public List<Company> companies { get; set; }
        //public List<EquipmentManufacturer> EquipmentManufacturers { get; set; }
        //public List<EquipmentCategory> EquipmentCategorys { get; set; }
    }
}
