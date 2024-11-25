using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UniSpace.Data.Models
{
    public class Proffesseur : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public List<Subject> TaughtSubjects { get; set; } = new List<Subject>();
    }
}
