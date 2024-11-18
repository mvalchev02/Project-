namespace UniSpace.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        public string Comments { get; set; }


    }
}
