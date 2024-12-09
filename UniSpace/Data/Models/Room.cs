using WebApplication1.Data.Models.Enums;

namespace WebApplication1.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public RoomType Type { get; set; }

    }
}
