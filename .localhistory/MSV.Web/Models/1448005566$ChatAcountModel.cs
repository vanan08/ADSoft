using ADSoft.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CMC.Northwind.Models
{
    public class ChatAcount
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string NickName { get; set; }
    }
}
