using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("tTrendGroup")]
    public class TrendGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CollectDataTypeID { get; set; }
    }
}
