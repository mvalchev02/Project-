using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Specialty> GetAllSpecialties()
        {
            return _context.Specialties.ToList();
        }

        public Specialty GetSpecialtyById(int id)
        {
            return _context.Specialties.Find(id);
        }

        public void AddSpecialty(Specialty specialty)
        {
            _context.Specialties.Add(specialty);
            _context.SaveChanges();
        }

        public void UpdateSpecialty(Specialty specialty)
        {
            _context.Specialties.Update(specialty);
            _context.SaveChanges();
        }

        public void DeleteSpecialty(int id)
        {
            var specialty = _context.Specialties.Find(id);
            if (specialty != null)
            {
                _context.Specialties.Remove(specialty);
                _context.SaveChanges();
            }
        }
    }
}
