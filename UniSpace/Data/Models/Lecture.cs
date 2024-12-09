using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Models
{
    public class Lecture
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Room { get; set; }

        [Required]
        [StringLength(50)]
        public string Specialty { get; set; }

        [Required]
        public int Course { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }
        public string? UserId { get; set; }
    }
}