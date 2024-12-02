using Microsoft.AspNetCore.Mvc;
using UniSpace.Data;
using UniSpace.Data.Models;

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

    public IActionResult Details(int id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null)
        {
            return NotFound();
        }
        return View("Details",subject);
    }

    [HttpGet]
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
            return RedirectToAction("Index");
        }
        return View("Create", subject);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null)
        {
            return NotFound();
        }
        return View(subject);
    }

    [HttpPost]
    public IActionResult Edit(Subject subject)
    {
        if (ModelState.IsValid)
        {
            _context.Subjects.Update(subject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Edit", subject);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null)
        {
            return NotFound();
        }
        _context.Subjects.Remove(subject);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
