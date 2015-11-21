using ADSoft.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CMC.Northwind.Models
{
    public class BankModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Logo { get; set; }
        public string CTK { get; set; }
        public string STK { get; set; }
    }
}
