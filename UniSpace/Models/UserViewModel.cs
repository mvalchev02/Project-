using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UniSpace.Models
{
    public class AddUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public List<int> SubjectIds { get; set; } = new List<int>(); 
        public IEnumerable<SelectListItem> AvailableSubjects { get; set; } = new List<SelectListItem>();

        public string Specialty { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }

}
