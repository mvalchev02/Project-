using WebApplication1.Data.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

public class SubjectService : ISubjectService
{
    private readonly ApplicationDbContext _context;

    public SubjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subject>> GetSubjectsAsync(string searchString, int pageNumber, int pageSize)
    {
        var subjects = from s in _context.Subjects select s;

        if (!string.IsNullOrEmpty(searchString))
        {
            subjects = subjects.Where(s => s.Name.Contains(searchString));
        }

        return await PaginatedList<Subject>.CreateAsync(subjects.AsNoTracking(), pageNumber, pageSize);
    }

    public async Task<Subject> GetSubjectByIdAsync(int id)
    {
        return await _context.Subjects.FindAsync(id);
    }

    public async Task CreateSubjectAsync(Subject subject)
    {
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
    }

    public async Task EditSubjectAsync(Subject subject)
    {
        _context.Subjects.Update(subject);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubjectAsync(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject != null)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}
