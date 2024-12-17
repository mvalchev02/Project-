using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Models;

public class SubjectController : Controller
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    public async Task<IActionResult> Index(string searchString, int? pageNumber, string currentFilter)
    {
        ViewData["CurrentFilter"] = searchString;

        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        int pageSize = 5;
        var subjects = await _subjectService.GetSubjectsAsync(searchString, pageNumber ?? 1, pageSize);

        return View(subjects);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Subject subject)
    {
        if (ModelState.IsValid)
        {
            await _subjectService.CreateSubjectAsync(subject);
            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null) return NotFound();

        return View(subject);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Subject subject)
    {
        if (ModelState.IsValid)
        {
            await _subjectService.EditSubjectAsync(subject);
            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        if (subject == null) return NotFound();

        await _subjectService.DeleteSubjectAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
