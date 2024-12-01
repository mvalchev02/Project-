using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniSpace.Data.Models.Enums;

namespace UniSpace.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        [ForeignKey(nameof(Professor))]
        public string ProfessorId { get; set; }
        public Proffesseur Professor { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public string Note { get; set; }
    }
}
