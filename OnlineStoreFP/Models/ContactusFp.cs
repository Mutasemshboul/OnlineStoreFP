using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class ContactusFp
    {
        public decimal Contactid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        //public Contactdynmic contactdynmic { get; set; }
    }
}
