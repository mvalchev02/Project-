using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniSpace.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey(nameof(Specialty))]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public int Course { get; set; }
    }
}
