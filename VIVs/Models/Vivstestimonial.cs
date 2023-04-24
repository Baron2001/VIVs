using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivstestimonial
    {
        public decimal Id { get; set; }
        public int? Rating { get; set; }
        public string Opinion { get; set; }
        public string Status { get; set; }
        public decimal? Userid { get; set; }

        public virtual Vivsuser User { get; set; }
    }
}
