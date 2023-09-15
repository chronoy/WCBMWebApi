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
        public string LineName { get; set; }
        public string CompanyName { get; set; }
        public string StationName { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string? ProcessLocation { get; set; }
        public string SerialNumber { get; set; }
        public string Caliber { get; set; }
        public string? Range { get; set; }
        public string Accuracy { get; set; }
        public string Uncertainty { get; set; }
        public string PressureLevel { get;set; }
        
        public string? InsideDiameter { get; set; }
        public string? Length { get; set; }
        public string? KFactor { get; set; }
        public string? CommonFlow { get; set; }
        public string? InstallationCondition { get; set; }

        public string Customer { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string Status { get; set; }
        public string TradeProperty { get; set; }
        public string? Note { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime? ScapDate { get; set; }
    }
}
