using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADSoft.Web.Models
{
    public class EmailModel
    {
        [Required(ErrorMessage = "The field is required")]
        public string ToAddress { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
    }
}