using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Professor,Admin")]

        public IActionResult Index()
        {
            var specialties = _context.Specialties.ToList();
            return View(specialties);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _context.Specialties.Add(specialty);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var specialty = _context.Specialties.Find(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _context.Specialties.Update(specialty);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var specialty = _context.Specialties.Find(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var specialty = _context.Specialties.Find(id);
            if (specialty == null) return NotFound();

            _context.Specialties.Remove(specialty);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}
