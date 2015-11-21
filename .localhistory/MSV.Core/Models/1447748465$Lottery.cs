using System;
using System.Collections.Generic;

namespace ADSoft.Core.Models
{
    public partial class Lottery
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Jackpots { get; set; }
        public string First { get; set; }
        public string Second { get; set; }
        public string Third { get; set; }
        public string Fourth { get; set; }
        public string Fifth { get; set; }
        public string Six { get; set; }
        public string Seven { get; set; }
        public DateTime JackpotsDate { get; set; }
    }
}
