using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class UserloginFp
    {
        public decimal Loginid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? Roleid { get; set; }
        public decimal? Userid { get; set; }

        public virtual RolesFp Role { get; set; }
        public virtual UserFp User { get; set; }
    }
}
