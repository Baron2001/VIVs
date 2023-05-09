using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace VIVs.Models
{
    public partial class Vivsuser
    {
        public Vivsuser()
        {
            Vivsbookings = new HashSet<Vivsbooking>();
            Vivslogins = new HashSet<Vivslogin>();
            Vivsposts = new HashSet<Vivspost>();
            Vivstestimonials = new HashSet<Vivstestimonial>();
        }
        [NotMapped]
        public string FirstName { get; set; }
        [NotMapped]
        public string MiddleName { get; set; }
        [NotMapped]
        public string Surname { get; set; }
        [NotMapped]
        public string LastName { get; set; }

        public decimal Userid { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirmpassword { get; set; }
        public decimal? Phonenumber { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public string Gender { get; set; }
        public bool? Isdeleted { get; set; }
        public string Status { get; set; }
        public string Verifycode { get; set; }
        public decimal? Establishmentnationalnumber { get; set; }
        public string Otherscategory { get; set; }
        public string Address { get; set; }
        public decimal? Categorytypeid { get; set; }
        public decimal? Cityid { get; set; }
        public string Estabname { get; set; }

        public virtual Vivscategory Categorytype { get; set; }
        public virtual Vivscity City { get; set; }
        public virtual ICollection<Vivsbooking> Vivsbookings { get; set; }
        public virtual ICollection<Vivslogin> Vivslogins { get; set; }
        public virtual ICollection<Vivspost> Vivsposts { get; set; }
        public virtual ICollection<Vivstestimonial> Vivstestimonials { get; set; }
    }
}
