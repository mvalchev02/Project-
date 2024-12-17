using System.Collections.Generic;
using WebApplication1.Data.Models;

namespace WebApplication1.Services.Interfaces
{
    public interface ISpecialtyService
    {
        List<Specialty> GetAllSpecialties();
        Specialty GetSpecialtyById(int id);
        void AddSpecialty(Specialty specialty);
        void UpdateSpecialty(Specialty specialty);
        void DeleteSpecialty(int id);
    }
}
