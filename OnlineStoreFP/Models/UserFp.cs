using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class UserFp
    {
        public UserFp()
        {
            ProductuserFps = new HashSet<ProductuserFp>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            TestimonialFps = new HashSet<TestimonialFp>();
            UserloginFps = new HashSet<UserloginFp>();
            VisacarFps = new HashSet<VisacarFp>();
        }

        public decimal Userid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Password { get; set; }

        public virtual ICollection<ProductuserFp> ProductuserFps { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<TestimonialFp> TestimonialFps { get; set; }
        public virtual ICollection<UserloginFp> UserloginFps { get; set; }
        public virtual ICollection<VisacarFp> VisacarFps { get; set; }
    }
}
