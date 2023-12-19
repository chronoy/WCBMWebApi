using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [Table("tTag")]
    public class Tag
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int StationDeviceCollectID { get; set; }
        public bool Enable { get; set; }
        public double? HiHiLimit { get; set; }
        public double? HiLimit { get; set; }
        public double? LoLimit { get; set; }
        public double? LoLoLimit { get; set; }
        public bool IsTrend {  get; set; }
    }
    public class PDBTag
    {
        [Key]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Value { get; set; } = "????";
        public string Quality { get; set; } = "Uncertain";
        public bool Enable { get; set; }
        public double? HiHiLimit { get; set; }
        public double? HiLimit { get; set; }
        public double? LoLimit { get; set; }
        public double? LoLoLimit { get; set; }
        public string Status { get; set; } = "OK";
        public bool IsTrend { get; set; }

        public void GetStatus()
        {
            if (Enable && double.TryParse(Value, out var number))
            {
                switch (number)
                {
                    case double n when HiHiLimit == null && HiLimit != null && n > HiLimit:
                    case double v when HiLimit != null && v > HiLimit && HiHiLimit != null && v <= HiHiLimit:
                        Status = "Hi";
                        break;
                    case double n when LoLoLimit == null && LoLimit != null && n < LoLimit:
                    case double v when LoLimit != null && v < LoLimit && LoLoLimit != null && v >= LoLoLimit:
                        Status = "Lo";
                        break;
                    case double n when HiHiLimit != null && n > HiHiLimit:
                        Status = "HiHi";
                        break;
                    case double n when LoLoLimit != null && n < LoLoLimit:
                        Status = "LoLo";
                        break;
                }
            }
        }
    }
}
