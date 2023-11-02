using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tTrendTag")]
    public class TrendTag
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double HighLimit { get; set;}
        public double LowLimit { get; set; }
        public string? Description { get; set; }
        public string DeviceType { get; set; }
        public int DeviceID { get; set;}
        public string? DeviceName { get; set; }
        public string? StationName { get; set; }
    }
}
