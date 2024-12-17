using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using WebApplication1.Data.Models;
using WebApplication1.Data.Models.Enums;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [Authorize(Roles = "Professor,Admin")]
        public IActionResult Index()
        {
            var rooms = _roomService.GetAllRooms();
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
                _roomService.AddRoom(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var room = _roomService.GetRoomById(id);
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
                _roomService.UpdateRoom(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _roomService.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
