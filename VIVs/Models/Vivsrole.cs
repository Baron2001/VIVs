using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivsrole
    {
        public Vivsrole()
        {
            Vivslogins = new HashSet<Vivslogin>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Vivslogin> Vivslogins { get; set; }
    }
}
