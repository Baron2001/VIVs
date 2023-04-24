using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivslogin
    {
        public decimal Loginid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? Usersid { get; set; }
        public decimal? Rolesid { get; set; }

        public virtual Vivsrole Roles { get; set; }
        public virtual Vivsuser Users { get; set; }
    }
}
