﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSoft.Web.Models
{
    public class EmailModel
    {
        public string ToAddress { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
    }
}