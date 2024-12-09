using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subjects = _context.Subjects.ToList();
            return View(subjects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Subjects.Add(subject);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public IActionResult Edit(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return NotFound();

            return View(subject);
        }

        [HttpPost]
        public IActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Subjects.Update(subject);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public IActionResult Delete(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null) return NotFound();

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}
