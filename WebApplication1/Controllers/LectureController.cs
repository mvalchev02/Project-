using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1
{

    [Authorize]
    public class LecturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);   
            var lectures = await _context.Lectures
                                         .Where(l => l.UserId == userId)   
                                         .ToListAsync();

            return View(lectures);   
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lecture = await _context.Lectures
                .FirstOrDefaultAsync();

            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        public IActionResult Create()
        {
            var rooms = _context.Rooms.Select(r => r.Name).ToList();
            ViewBag.Rooms = new SelectList(rooms);

            var specialties = _context.Specialties.Select(s => s.Name).ToList();
            ViewBag.Specialties = new SelectList(specialties);

            var subjects = _context.Subjects.Select(c => c.Name).ToList();
            ViewBag.Subjects = new SelectList(subjects);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                 lecture.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(lecture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  
            }
            return View(lecture);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lecture = await _context.Lectures
                .FirstOrDefaultAsync();

            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lecture lecture)
        {
            if (id != lecture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                     _context.Update(lecture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureExists(lecture.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lecture);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lecture = await _context.Lectures
                .FirstOrDefaultAsync();

            if (lecture == null)
            {
                return NotFound();
            }

            return View(lecture);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lecture = await _context.Lectures
                .FirstOrDefaultAsync();

            if (lecture != null)
            {
                _context.Lectures.Remove(lecture);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LectureExists(int id)
        {
            return _context.Lectures.Any(e => e.Id == id);
        }
    }
}