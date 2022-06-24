using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStoreFP.Models
{
    public partial class RolesFp
    {
        public RolesFp()
        {
            UserloginFps = new HashSet<UserloginFp>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<UserloginFp> UserloginFps { get; set; }
    }
}
