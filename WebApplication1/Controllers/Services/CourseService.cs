using System.Collections.Generic;
using System.Linq;
using WebApplication1.Controllers.Services.Interfaces;
using WebApplication1.Data;
using WebApplication1.Data.Models;

namespace WebApplication1.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public bool DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return true;
        }
    }
}
