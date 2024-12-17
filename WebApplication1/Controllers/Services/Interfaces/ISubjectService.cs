using WebApplication1.Data.Models;

public interface ISubjectService
{
    Task<List<Subject>> GetSubjectsAsync(string searchString, int pageNumber, int pageSize);
    Task<Subject> GetSubjectByIdAsync(int id);
    Task CreateSubjectAsync(Subject subject);
    Task EditSubjectAsync(Subject subject);
    Task DeleteSubjectAsync(int id);
}
