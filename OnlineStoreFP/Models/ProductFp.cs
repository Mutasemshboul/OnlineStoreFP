using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class ProductFp
    {
        public ProductFp()
        {
            ProductuserFps = new HashSet<ProductuserFp>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public decimal Productid { get; set; }
        public string Productname { get; set; }
        public decimal? Sale { get; set; }
        public decimal? Price { get; set; }
        public decimal? Storeid { get; set; }
        public decimal? Quantity { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<ProductuserFp> ProductuserFps { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
