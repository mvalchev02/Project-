using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniSpace.Data.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        [Required]
        [StringLength(20)]
        public string Role { get; set; }
        [ForeignKey(nameof(Reservations))]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }

}
