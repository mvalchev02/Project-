using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int? pageNumber, string currentFilter)
        {
            ViewData["CurrentFilter"] = searchString;

            var subjects = from s in _context.Subjects select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                subjects = subjects.Where(s => s.Name.Contains(searchString));
            }

            int pageSize = 5;
            return View(await PaginatedList<Subject>.CreateAsync(subjects.AsNoTracking(), pageNumber ?? 1, pageSize));

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
