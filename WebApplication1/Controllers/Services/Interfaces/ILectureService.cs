using WebApplication1.Data.Models;

namespace WebApplication1.Controllers.Services.Interfaces
{
    public interface ILectureService
    {
        Task<List<Lecture>> GetLecturesAsync(string userId);
        Task<Lecture> GetLectureByIdAsync(int id, string userId);
        Task CreateLectureAsync(Lecture lecture);
        Task EditLectureAsync(Lecture lecture);
        Task DeleteLectureAsync(int id, string userId);
    }

}
