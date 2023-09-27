using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Models
{
    [Table("tArea")]
    public  class Area
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? AbbrName { get; set; }
        public int CompanyID { get; set; }
    }
}
