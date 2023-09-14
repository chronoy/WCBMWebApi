using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("tRealtimeDiagnosticDataEquipment")]
    [Keyless]
    public class StationEquipmentDiagnosticData
    {
        [Key]
        public int ID { get; set; }
        [Column("CurrentDT")]
        public string DateTime { get; set; }
        [Column("Result")]
        public int ResultID { get; set; }
        [NotMapped]
        public string EquipmentName { get; set; }
        [NotMapped]
        public string EquipmentCategoryI { get; set; }
        [NotMapped]
        public string Manufacturer { get; set; }
        [NotMapped]
        public string Model { get; set; }
    }
}
