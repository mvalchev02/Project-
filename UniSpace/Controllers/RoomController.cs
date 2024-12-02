using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSpace.Data.Models.Enums;
using UniSpace.Data.Models;
using UniSpace.Data;

[Authorize(Roles = "Admin")]
public class RoomController : Controller
{
    private readonly ApplicationDbContext _context;

    public RoomController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var rooms = _context.Rooms.ToList();
        return View(rooms);
    }

    public IActionResult Details(int id)
    {
        var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        return View("Details", room);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        if (ModelState.IsValid)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
        return View("Room/Create", room);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
        return View("Edit",room);
    }

    [HttpPost]
    public IActionResult Edit(Room room)
    {
        if (ModelState.IsValid)
        {
            var existingRoom = _context.Rooms.FirstOrDefault(r => r.Id == room.Id);
            if (existingRoom != null)
            {
                existingRoom.Name = room.Name;
                existingRoom.Type = room.Type;
                existingRoom.Capacity = room.Capacity;
                existingRoom.Equipment = room.Equipment;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
        return View(room);
    }



    [HttpPost]
    public IActionResult Delete(int id)
    {
        var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        _context.Rooms.Remove(room);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
