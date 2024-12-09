using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var specialties = _context.Specialties.ToList();
            return View(specialties);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public IActionResult Edit(int id)
        {
            var specialty = _context.Specialties.Find(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [HttpPost]
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

        public IActionResult Delete(int id)
        {
            var specialty = _context.Specialties.Find(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [HttpPost, ActionName("Delete")]
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
