using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivscity
    {
        public Vivscity()
        {
            Vivsusers = new HashSet<Vivsuser>();
        }

        public decimal Cityid { get; set; }
        public string City { get; set; }

        public virtual ICollection<Vivsuser> Vivsusers { get; set; }
    }
}
