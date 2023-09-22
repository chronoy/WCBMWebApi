using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Table("tCompany")]
    public class Company
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? AbbrName { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set;}
        public string? Address { get; set;} 
        public string? PostalCode { get; set;}
        public string? Description { get; set;}
        public int BelongsID { get; set; }
        public int EntrustmentDepartmentID { get; set; }
        public int Property { get; set; }
    }
}
