using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniSpace.Data.Models.Enums;

namespace UniSpace.Data.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public UserInfo User { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public ReservationStatus Status { get; set; } 

        [Required]
        [MaxLength(100)]
        public string ReasonForReservation { get; set; } 

        [ForeignKey(nameof(Specialty))]
        public int? SpecialtyId { get; set; } 
        public Specialty Specialty { get; set; }

        public CoursesEnum Course { get; set; } 

        [ForeignKey(nameof(Subject))]
        public int? SubjectId { get; set; } 
        public Subject Subject { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }
    }
}
