using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Data.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [Authorize(Roles = "Professor,Admin")]
        public IActionResult Index()
        {
            var specialties = _specialtyService.GetAllSpecialties();
            return View(specialties);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _specialtyService.AddSpecialty(specialty);
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var specialty = _specialtyService.GetSpecialtyById(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _specialtyService.UpdateSpecialty(specialty);
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var specialty = _specialtyService.GetSpecialtyById(id);
            if (specialty == null) return NotFound();

            return View(specialty);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _specialtyService.DeleteSpecialty(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
