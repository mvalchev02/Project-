using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniSpace.Data.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Type { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        [StringLength(30)]
        public string Location { get; set; }
        [Required]
        [StringLength(50)]
        public string Equipment { get; set; }
        [ForeignKey(nameof(Reservations))]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

