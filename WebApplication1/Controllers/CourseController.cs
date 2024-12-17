using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Services.Interfaces;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var courses = _courseService.GetAllCourses();
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.AddCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public IActionResult Edit(int id)
        {
            var course = _courseService.GetCourseById(id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.UpdateCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = _courseService.GetCourseById(id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var success = _courseService.DeleteCourse(id);
            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
