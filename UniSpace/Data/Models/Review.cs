using System;
using System.ComponentModel.DataAnnotations;
using UniSpace.Data.Models;

public class Review
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }
    public Room Room { get; set; }
    [Required]
    public string UserId { get; set; }
    public User User { get; set; }
    [Required]

    public int Rating { get; set; } 

    public string Comment { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
}
