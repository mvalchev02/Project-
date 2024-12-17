using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Controllers.Services.Interfaces;
using WebApplication1.Data.Models;
using WebApplication1.Data;

[Authorize(Roles = "Professor")]
public class LecturesController : Controller
{
    private readonly ILectureService _lectureService;

    private readonly ApplicationDbContext _context;

    public LecturesController(ILectureService lectureService, ApplicationDbContext context)
    {
        _lectureService = lectureService;
        _context = context;

    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var lectures = await _lectureService.GetLecturesAsync(userId);

        return View(lectures);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var lecture = await _lectureService.GetLectureByIdAsync(id.Value, userId);

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
            await _lectureService.CreateLectureAsync(lecture);
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
        var lecture = await _lectureService.GetLectureByIdAsync(id.Value, userId);

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

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var existingLecture = await _lectureService.GetLectureByIdAsync(id, userId);

        if (existingLecture == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                existingLecture.Title = lecture.Title;
                existingLecture.Date = lecture.Date;
                existingLecture.Room = lecture.Room;
                existingLecture.Specialty = lecture.Specialty;
                existingLecture.Course = lecture.Course;
                existingLecture.Subject = lecture.Subject;

                await _lectureService.EditLectureAsync(existingLecture);
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
        var lecture = await _lectureService.GetLectureByIdAsync(id.Value, userId);

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
        await _lectureService.DeleteLectureAsync(id, userId);

        return RedirectToAction(nameof(Index));
    }

    private bool LectureExists(int id)
    {
        return _context.Lectures.Any(e => e.Id == id);
    }
}
