using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class Store
    {
        public Store()
        {
            ProductFps = new HashSet<ProductFp>();
        }

        public decimal Storeid { get; set; }
        public string Storename { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Categoryid { get; set; }

        public virtual CategoryFp Category { get; set; }
        public virtual ICollection<ProductFp> ProductFps { get; set; }
    }
}
