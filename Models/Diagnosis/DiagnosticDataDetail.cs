using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Keyless]
    public class DiagnosticDataDetail
    {
        public string Name { get; set; }
        public string Result { get; set; }
        public string Value {get;set;}
    }
}
