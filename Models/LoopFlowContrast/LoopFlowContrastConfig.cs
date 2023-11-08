using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tLoopFlowContrastConfig")]
    public class LoopFlowContrastConfig
    {
        [Key]
        public int ID { get; set; }


        public int StationID { get; set; }


        public int InUseLoopID { get; set; }


        public int ContrastLoopID { get; set; }

        [Column("ContrastState")]
        public int ContrastStateID { get; set; }


        public DateTime StartDateTime { get; set; }


        public DateTime? EndDateTime { get; set; }

      
        [NotMapped]
        public string? StationName { get; set; }

        [NotMapped]
        public string? InUseLoopName { get; set; }

        [NotMapped]
        public string? ContrastLoopName { get; set; }

        [NotMapped]
        public string? ContrastState { get; set; }

    }
}
