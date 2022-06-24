using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class TestimonialFp
    {
        public decimal Testimonialid { get; set; }
        public string Message { get; set; }
        public decimal? Userid { get; set; }

        public virtual UserFp User { get; set; }
    }
}
