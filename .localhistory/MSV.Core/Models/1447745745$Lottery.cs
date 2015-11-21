using System;
using System.Collections.Generic;

namespace ADSoft.Core.Models
{
    public partial class Lottery
    {

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
