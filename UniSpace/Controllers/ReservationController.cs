using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniSpace.Data;
using UniSpace.Data.Models;
using UniSpace.Data.Models.Enums;

using System.Linq;
using System.Threading.Tasks;

namespace UniSpace.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public JsonResult GetCoursesBySpecialty()
        {
            var courses = Enum.GetValues(typeof(CoursesEnum))
                              .Cast<CoursesEnum>()
                              .Select(c => new { Id = (int)c, Year = c.ToString() })
                              .ToList();

            return Json(courses);
        }


        public JsonResult GetSubjectsByCourse(int courseId)
        {
            CoursesEnum selectedCourse = (CoursesEnum)courseId;

            var subjects = _context.Subjects
                                    .Where(s => s.Course == selectedCourse) 
                                    .Select(s => new { s.Id, s.Name })
                                    .ToList();

            return Json(subjects);
        }



        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations
                                             .Include(r => r.Specialty)
                                             .Include(r => r.Course)
                                             .Include(r => r.Subject)
                                             .ToListAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                                            .Include(r => r.Specialty)
                                            .Include(r => r.Course)
                                            .Include(r => r.Subject)
                                            .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Name", reservation.SpecialtyId);
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReasonForReservation,SpecialtyId,CourseId,SubjectId,Notes")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Name", reservation.SpecialtyId);
            return View(reservation);
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        public JsonResult GetSpecialtiesByType(string type)
        {
            var specialties = _context.Specialties
                                      .Where(s => s.Name == type)
                                      .Select(s => new { s.Id, s.Name })
                                      .ToList();

            return Json(specialties);
        }
    }
}
