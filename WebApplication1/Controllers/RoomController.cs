using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data.Models.Enums;
using WebApplication1.Data.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Professor,Admin")]
        public IActionResult Index()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
                .Cast<RoomType>()
                .Select(rt => new SelectListItem { Value = rt.ToString(), Text = rt.ToString() })
                .ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

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

        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

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
