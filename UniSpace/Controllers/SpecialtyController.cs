using Microsoft.AspNetCore.Mvc;
using UniSpace.Data.Models;
using UniSpace.Data;
using Microsoft.EntityFrameworkCore;

public class SpecialtyController : Controller
{
    private readonly ApplicationDbContext _context;

    public SpecialtyController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var specialties = _context.Specialties.Include(s => s.MainSubjects).ToList();
        return View(specialties);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Subjects = _context.Subjects.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Specialty specialty, List<int> MainSubjects)
    {
        if (ModelState.IsValid)
        {
            if (MainSubjects != null)
            {
                foreach (var subjectId in MainSubjects)
                {
                    var subject = _context.Subjects.FirstOrDefault(s => s.Id == subjectId);
                    if (subject != null)
                    {
                        specialty.MainSubjects.Add(subject);
                    }
                }
            }

            _context.Specialties.Add(specialty);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Subjects = _context.Subjects.ToList();
        return View(specialty);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var specialty = _context.Specialties.Include(s => s.MainSubjects)
                                            .FirstOrDefault(s => s.Id == id);
        if (specialty == null)
        {
            return NotFound();
        }

        ViewBag.Subjects = _context.Subjects.ToList();
        return View(specialty);
    }

    [HttpPost]
    public IActionResult Edit(int id, Specialty specialty, List<int> MainSubjects)
    {
        if (id != specialty.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var specialtyToUpdate = _context.Specialties
                .Include(s => s.MainSubjects)
                .FirstOrDefault(s => s.Id == id);

            if (specialtyToUpdate == null)
            {
                return NotFound();
            }

            specialtyToUpdate.MainSubjects.Clear();

            foreach (var subjectId in MainSubjects)
            {
                var subject = _context.Subjects.FirstOrDefault(s => s.Id == subjectId);
                if (subject != null)
                {
                    specialtyToUpdate.MainSubjects.Add(subject);
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        ViewBag.Subjects = _context.Subjects.ToList();
        return View(specialty);
    }



    public IActionResult Details(int id)
    {
        var specialty = _context.Specialties.Include(s => s.MainSubjects)
                                            .FirstOrDefault(s => s.Id == id);
        if (specialty == null)
        {
            return NotFound();
        }
        return View("Details",specialty);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var specialty = _context.Specialties.FirstOrDefault(s => s.Id == id);
        if (specialty == null)
        {
            return NotFound();
        }

        _context.Specialties.Remove(specialty);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
