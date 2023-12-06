using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Models
{
    [Table("tStationEquipment")]
    public class StationEquipment
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string AbbrName { get; set; }

        public int CollectDataTypeID { get; set; }
        public int EquipmentCategoryID { get;set; }
        public int StationID { get; set; }
        public int LineID { get; set; }
    public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int OrderNumber { get; set; }
        [NotMapped]
        public string StationName { get; set; }
        [NotMapped]
        public string StationAbbrName { get; set; }
        [NotMapped]
        public Dictionary<string, Tag> Tags { get; set; }
        [NotMapped]
        public StationEquipmentDiagnosticData DiagnosticData { get; set; }
        [NotMapped]
        public string EquipmentCategoryName { get; set; }

        [NotMapped]
        public List<PDBTag> EquipmentTags { get; set; } = new List<PDBTag>();

        [NotMapped]
        public int AlarmCount { get; set; }

        [NotMapped]
        public string EquipmentStatus { get; set; }
        [NotMapped]
        public Dictionary<string, object> EquipmentStatistics { get; set; } = new Dictionary<string, object>();
    }
}
