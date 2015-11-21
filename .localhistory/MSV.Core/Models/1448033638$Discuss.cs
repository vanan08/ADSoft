using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADSoft.Core.Models
{
    public partial class Discuss
    { 
        public string Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}