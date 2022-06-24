using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class CategoryFp
    {
        public CategoryFp()
        {
            Stores = new HashSet<Store>();
        }

        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
