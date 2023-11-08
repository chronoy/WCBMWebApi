using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("tLoopFlowContrastRecord")]
    public class LoopFlowContrastRecord
    {
        [Key]
        public int ID { get; set; }


        public int LoopFlowContrastConfigID { get; set; }


        public int StationID { get; set; }


        public int InUseLoopID { get; set; }


        public int ContrastLoopID { get; set; }


        public DateTime StartDateTime { get; set; }


        public DateTime EndDateTime { get; set; }


        public double? InUseLoopStartForwordGrossCumulative { get; set; }


        public double? ContrastLoopStartForwordGrossCumulative { get; set; }


        public double? InUseLoopStartForwordStandardCumulative { get; set; }


        public double? ContrastLoopStartForwordStandardCumulative { get; set; }


        public double? InUseLoopStartPressure { get; set; }


        public double? ContrastLoopStartPressure { get; set; }


        public double? InUseLoopStartTemperature { get; set; }


        public double? ContrastLoopStartTemperature { get; set; }


        public double? InUseLoopEndForwordGrossCumulative { get; set; }


        public double? ContrastLoopEndForwordGrossCumulative { get; set; }


        public double? InUseLoopEndForwordStandardCumulative { get; set; }


        public double? ContrastLoopEndForwordStandardCumulative { get; set; }


        public double? InUseLoopEndPressure { get; set; }


        public double? ContrastLoopEndPressure { get; set; }


        public double? InUseLoopEndTemperature { get; set; }


        public double? ContrastLoopEndTemperature { get; set; }


        public double? InUseLoopFCCalculatedVOSDeviationRate { get; set; }


        public double? ContrastLoopFCCalculatedVOSDeviationRate { get; set; }


        public double? InUseLoopVOSMaxDeviation { get; set; }


        public double? ContrastLoopVOSMaxDeviation { get; set; }


        public double? ForwordStandardContrastResult { get; set; }


        public double? ForwordGrossContrastResult { get; set; }

        [Column("LoopContrastStatus")]
        public int LoopContrastStatusID { get; set; }
    }
}
