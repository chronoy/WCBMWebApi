using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("tStation")]
    public class Station
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string AbbrName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? Description { get; set; }
        public int AreaID { get; set; }
        public int CollectorID { get; set; }
        [NotMapped]
        public List<StationLoop> Loops { get; set; } = new List<StationLoop>();
    }
}

