﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivshome
    {
        public decimal Id { get; set; }
        public string Image1 { get; set; }
        public string Title1 { get; set; }
        public string Image2 { get; set; }
        public string Title2 { get; set; }
        public string Image3 { get; set; }
        public string Title3 { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile1 { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile2 { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile3 { get; set; }
    }
}
