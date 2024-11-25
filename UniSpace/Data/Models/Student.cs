using System.ComponentModel.DataAnnotations;
using UniSpace.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace UniSpace.Data.Models
{
    public class Student : IdentityUser
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
        [Required]
        public string Specialty { get; set; }

        [Required]
        public CoursesEnum Course { get; set; }
    }
}
