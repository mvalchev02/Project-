using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniSpace.Data.Models
{
    public class Specialty
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
