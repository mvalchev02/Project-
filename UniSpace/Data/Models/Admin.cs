using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UniSpace.Data.Models
{
    public class Admin : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string? LastName { get; set; }
        public double Salary { get; set; }
    }
}
