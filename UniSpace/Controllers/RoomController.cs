using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data.Models.Enums;
using WebApplication1.Data.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
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

        public IActionResult Create()
        {
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
                .Cast<RoomType>()
                .Select(rt => new SelectListItem { Value = rt.ToString(), Text = rt.ToString() })
                .ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public IActionResult Edit(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();

            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
                .Cast<RoomType>()
                .Select(rt => new SelectListItem { Value = rt.ToString(), Text = rt.ToString() })
                .ToList();

            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Rooms.Update(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public IActionResult Delete(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}
