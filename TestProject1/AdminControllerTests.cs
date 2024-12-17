using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Identity;
using UniSpace.Controllers;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace UniSpace.Tests.Controllers
{
    [TestFixture]
    public class AdminControllerTests
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private AdminController _controller;

        [SetUp]
        public void Setup()
        {
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null
            );

            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                null, null, null, null
            );

            _controller = new AdminController(_mockUserManager.Object, _mockRoleManager.Object);
        }

        [TearDown]
        public void TearDown()
        {
             _controller?.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewWithUsersAndRoles()
        {
             var mockUsers = new List<IdentityUser>
            {
                new IdentityUser { Id = "1", UserName = "user1" },
                new IdentityUser { Id = "2", UserName = "user2" }
            };

            _mockUserManager.Setup(m => m.Users).Returns(mockUsers.AsQueryable());
            _mockUserManager.Setup(m => m.GetRolesAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(new List<string> { "Admin" });

             var result = await _controller.Index() as ViewResult;
            var model = result?.Model as List<UserWithRolesViewModel>;

             Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual("user1", model[0].User.UserName);
            Assert.AreEqual("Admin", model[0].Roles.First());
        }

        [Test]
        public async Task AddToRole_ValidUser_RedirectsToIndex()
        {
             var mockUser = new IdentityUser { Id = "1", UserName = "user1" };

            _mockUserManager.Setup(m => m.FindByIdAsync("1")).ReturnsAsync(mockUser);
            _mockUserManager.Setup(m => m.AddToRoleAsync(mockUser, "Professor"))
                .ReturnsAsync(IdentityResult.Success);

             var result = await _controller.AddToRole("1", "Professor") as RedirectToActionResult;

             Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            _mockUserManager.Verify(m => m.AddToRoleAsync(mockUser, "Professor"), Times.Once);
        }
 
        [Test]
        public async Task AddProfessor_InvalidData_ReturnsViewWithError()
        {
            var email = "";
            var password = "";

             var result = await _controller.AddProfessor(email, password) as ViewResult;

             Assert.IsNotNull(result);
            Assert.IsFalse(_controller.ModelState.IsValid);
            Assert.AreEqual(1, _controller.ModelState.ErrorCount);
            Assert.AreEqual("Email и парола са задължителни.", _controller.ModelState[""].Errors[0].ErrorMessage);
        }


        [Test]
        public async Task AddProfessor_CreationFails_ReturnsViewWithErrors()
        {
             var email = "professor@example.com";
            var password = "WeakPassword";
            var mockProfessor = new IdentityUser { Email = email, UserName = email };

            _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password is too weak" }));

             var result = await _controller.AddProfessor(email, password) as ViewResult;

             Assert.IsNotNull(result);
            Assert.AreEqual(1, _controller.ModelState.ErrorCount);
            Assert.AreEqual("Password is too weak", _controller.ModelState[""].Errors[0].ErrorMessage);
        }
    }
}
