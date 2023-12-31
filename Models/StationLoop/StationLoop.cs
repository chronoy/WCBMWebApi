﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Models
{
    [Table("tStationLoop")]
    public class StationLoop
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string AbbrName { get; set; }
        public int CollectDataTypeID { get; set; }
        public int EquipmentCategoryID { get; set; }
        public int StationID { get; set; }
        public int LineID { get; set; }
 
        public string Caliber { get; set; }
        public string Customer { get; set; }
        public string FlowmeterManufacturer { get; set; }
        public string FlowmeterModel { get; set; }  
        public string FlowComputerManufacturer { get; set; }
        public string FlowComputerModel { get; set; }
        public int OrderNumber { get; set; }

        [NotMapped]
        public Dictionary<string,Tag> Tags { get; set; }
        [NotMapped]
        public string EquipmentCategoryName { get; set; }

        [NotMapped]
        public string LoopStatus { get; set; }

        [NotMapped]
        public int AlarmCount { get; set; }

        [NotMapped]
        public bool StandardParameterStatus { get; set;}

        [NotMapped]
        public List<PDBTag> LoopTags { get; set; }=new List<PDBTag>();
        [NotMapped]
        public Dictionary<string, object> LoopStatistics { get; set; } = new Dictionary<string, object>();
    }
}
