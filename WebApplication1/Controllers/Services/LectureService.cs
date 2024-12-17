using WebApplication1.Controllers.Services.Interfaces;
using WebApplication1.Data.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

public class LectureService : ILectureService
{
    private readonly ApplicationDbContext _context;

    public LectureService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lecture>> GetLecturesAsync(string userId)
    {
        return await _context.Lectures
            .Where(l => l.UserId == userId)
            .ToListAsync();
    }

    public async Task<Lecture> GetLectureByIdAsync(int id, string userId)
    {
        return await _context.Lectures
            .Where(l => l.Id == id && l.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task CreateLectureAsync(Lecture lecture)
    {
        _context.Lectures.Add(lecture);
        await _context.SaveChangesAsync();
    }

    public async Task EditLectureAsync(Lecture lecture)
    {
        _context.Lectures.Update(lecture);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLectureAsync(int id, string userId)
    {
        var lecture = await _context.Lectures
            .Where(l => l.Id == id && l.UserId == userId)
            .FirstOrDefaultAsync();

        if (lecture != null)
        {
            _context.Lectures.Remove(lecture);
            await _context.SaveChangesAsync();
        }
    }
}
