using WebApplication1.Data.Models;

namespace WebApplication1.Controllers.Services.Interfaces
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();
        Course GetCourseById(int id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        bool DeleteCourse(int id);
    }
}
