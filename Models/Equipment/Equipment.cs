using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [JsonObject(MemberSerialization.OptOut)]
    [Table("tEquipment")]
    public class Equipment
    {
        public int ID { get; set; }
        public int StationID { get; set; }
        public int LoopID { get; set; }
        public string EquipmentCategory { get; set; }
        public string ProcessLocation { get; set; }
        public string SerialNumber { get; set; }
        public string Caliber { get; set; }
        public string AccuracyLevel { get; set; }
        public string PressureLevel { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? ValidBeginDate { get; set; }
        public DateTime? ValidEndDate { get; set; }
    }
}
