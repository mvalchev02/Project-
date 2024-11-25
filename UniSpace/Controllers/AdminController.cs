using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniSpace.Data.Models;
using UniSpace.Data.Models.Enums;
using UniSpace.Models;

namespace UniSpace.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string userType, string email, string password, string firstName, string lastName, string phone, string specialty, CoursesEnum? course)
        {
            if (string.IsNullOrEmpty(userType) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Всички задължителни полета трябва да са попълнени.");
                return View();
            }

            IdentityUser newUser;
            if (userType == "Professor")
            {
                newUser = new Proffesseur
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone
                };

                await _userManager.CreateAsync(newUser, password);
                await _userManager.AddToRoleAsync(newUser, "Professor");
            }
            else if (userType == "Student")
            {
                newUser = new Student
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone,
                    Specialty = specialty,
                    Course = course.Value
                };

                await _userManager.CreateAsync(newUser, password);
                await _userManager.AddToRoleAsync(newUser, "Student");
            }
            else
            {
                ModelState.AddModelError("", "Невалиден тип потребител.");
                return View();
            }

            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();

            var userViewModels = new List<AddUserViewModel>();

            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                userViewModels.Add(new AddUserViewModel
                {
                    Email = user.Email,
                    Role = role
                });
            }

            return View(userViewModels);
        }


    }
}
