using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivsbooking
    {
        public decimal Bookid { get; set; }
        public DateTime? Booktime { get; set; }
        public decimal? Postid { get; set; }
        public decimal? Userid { get; set; }

        public virtual Vivspost Post { get; set; }
        public virtual Vivsuser User { get; set; }
    }
}
