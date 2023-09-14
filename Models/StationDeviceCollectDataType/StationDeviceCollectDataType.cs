using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    [Table("tStationDeviceCollectDataType")]
    public class StationDeviceCollectDataType
    {
        [Key]
        public int ID { get; set; }
        public string Manufacturer { get; set; }
        public string DeviceType { get; set; }
        public int CollectType { get; set; }
        public string Description { get; set; }
    }
}
