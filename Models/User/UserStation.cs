using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tUserStation")]
    public class UserStation
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int StationID { get; set; }
    }
}
