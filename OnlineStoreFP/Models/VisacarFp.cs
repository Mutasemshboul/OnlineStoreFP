using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class VisacarFp
    {
        public decimal Visaid { get; set; }
        public string Numbercard { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Userid { get; set; }

        public virtual UserFp User { get; set; }
    }
}
