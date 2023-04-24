using System;
using System.Collections.Generic;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivscontactu
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public long? PhoneNumber { get; set; }
    }
}
