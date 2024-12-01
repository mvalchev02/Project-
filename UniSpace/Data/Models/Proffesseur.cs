using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniSpace.Data.Models
{
    public class Proffesseur : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string? LastName { get; set; }
        public double Salary { get; set; }
        public List<Subject> TaughtSubjects { get; set; } = new List<Subject>();
    }
}
