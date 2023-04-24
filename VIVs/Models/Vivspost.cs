using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivspost
    {
        public Vivspost()
        {
            Vivsbookings = new HashSet<Vivsbooking>();
        }

        public decimal Postid { get; set; }
        public decimal? Numberofitem { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? Postdate { get; set; }
        public bool? Isdeleted { get; set; }
        public decimal? Usersid { get; set; }

        public virtual Vivsuser Users { get; set; }
        public virtual ICollection<Vivsbooking> Vivsbookings { get; set; }
    }
}
