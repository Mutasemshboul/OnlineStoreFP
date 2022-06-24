using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class ShoppingCart
    {
        public decimal? Userid { get; set; }
        public decimal? Productid { get; set; }
        public decimal Id { get; set; }
        public decimal? Quantity { get; set; }

        public virtual ProductFp Product { get; set; }
        public virtual UserFp User { get; set; }
    }
}
