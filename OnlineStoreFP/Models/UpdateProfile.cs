using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreFP.Models
{
    public class UpdateProfile
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public string Password { get; set; }
    }
}
