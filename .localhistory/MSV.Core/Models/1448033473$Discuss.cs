using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADSoft.Core.Models
{
    public partial class Bank
    { 
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Logo { get; set; }
        public string CTK { get; set; }
        public string STK { get; set; }
    }
}