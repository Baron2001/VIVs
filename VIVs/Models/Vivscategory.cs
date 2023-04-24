using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivscategory
    {
        public Vivscategory()
        {
            Vivsusers = new HashSet<Vivsuser>();
        }

        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Categoryimagepath { get; set; }

        public virtual ICollection<Vivsuser> Vivsusers { get; set; }
    }
}
