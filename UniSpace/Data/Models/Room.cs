using System.ComponentModel.DataAnnotations;
using UniSpace.Data.Models.Enums;

namespace UniSpace.Data.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public RoomType Type { get; set; }
        public int Capacity { get; set; }
        public string Equipment { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
