using System.Collections.Generic;
using WebApplication1.Data.Models;

namespace WebApplication1.Services.Interfaces
{
    public interface IRoomService
    {
        List<Room> GetAllRooms();
        Room GetRoomById(int id);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
    }
}
