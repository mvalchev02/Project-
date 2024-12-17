namespace WebApplication1.Controllers.Services
{
    using global::WebApplication1.Data.Models;
    using global::WebApplication1.Data;
    using global::WebApplication1.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
 

    namespace WebApplication1.Services
    {
        public class RoomService : IRoomService
        {
            private readonly ApplicationDbContext _context;

            public RoomService(ApplicationDbContext context)
            {
                _context = context;
            }

            public List<Room> GetAllRooms()
            {
                return _context.Rooms.ToList();
            }

            public Room GetRoomById(int id)
            {
                return _context.Rooms.Find(id);
            }

            public void AddRoom(Room room)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
            }

            public void UpdateRoom(Room room)
            {
                _context.Rooms.Update(room);
                _context.SaveChanges();
            }

            public void DeleteRoom(int id)
            {
                var room = _context.Rooms.Find(id);
                if (room != null)
                {
                    _context.Rooms.Remove(room);
                    _context.SaveChanges();
                }
            }
        }
    }

}
