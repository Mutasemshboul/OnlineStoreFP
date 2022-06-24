using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class ProductuserFp
    {
        public decimal Productuserid { get; set; }
        public DateTime? Datefrom { get; set; }
        public DateTime? Dateto { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Productid { get; set; }
        public decimal? Userid { get; set; }

        public virtual ProductFp Product { get; set; }
        public virtual UserFp User { get; set; }
    }
}
