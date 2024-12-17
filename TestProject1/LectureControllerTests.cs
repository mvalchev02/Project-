using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Controllers.Services.Interfaces;
using WebApplication1.Data.Models;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class LecturesControllerTests
    {
        private Mock<ILectureService> _mockLectureService;
        private LecturesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLectureService = new Mock<ILectureService>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }));

            _controller = new LecturesController(_mockLectureService.Object, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = user
                    }
                }
            };
        }

        [Test]
        public async Task Index_ReturnsViewResult_WithListOfLectures()
        {
             var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, Title = "Lecture 1" },
                new Lecture { Id = 2, Title = "Lecture 2" }
            };

            _mockLectureService
                .Setup(service => service.GetLecturesAsync(It.IsAny<string>()))
                .ReturnsAsync(lectures);

             var result = await _controller.Index();

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult, "Index should return a ViewResult");
            var model = viewResult.Model as IEnumerable<Lecture>;
            Assert.IsNotNull(model, "Model should be a list of lectures");
            Assert.AreEqual(2, model.Count());
        }
        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
        [Test]
        public async Task Details_LectureExists_ReturnsViewResult()
        {
             var lecture = new Lecture { Id = 1, Title = "Test Lecture" };

            _mockLectureService
                .Setup(service => service.GetLectureByIdAsync(1, It.IsAny<string>()))
                .ReturnsAsync(lecture);

             var result = await _controller.Details(1);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult, "Details should return a ViewResult for an existing lecture");
            var model = viewResult.Model as Lecture;
            Assert.IsNotNull(model, "Model should be of type Lecture");
            Assert.AreEqual("Test Lecture", model.Title);
        }

        [Test]
        public async Task Details_LectureNotFound_ReturnsNotFound()
        {
             _mockLectureService
                .Setup(service => service.GetLectureByIdAsync(1, It.IsAny<string>()))
                .ReturnsAsync((Lecture)null);

             var result = await _controller.Details(1);

             Assert.IsInstanceOf<NotFoundResult>(result, "Details should return NotFound for a missing lecture");
        }

        [Test]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
             var lecture = new Lecture { Title = "New Lecture" };

             var result = await _controller.Create(lecture);

             _mockLectureService.Verify(service => service.CreateLectureAsync(lecture), Times.Once);
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult, "Create should redirect to Index on success");
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public async Task Edit_Post_ValidModel_UpdatesLectureAndRedirects()
        {
             var lecture = new Lecture { Id = 1, Title = "Updated Lecture" };

            _mockLectureService
                .Setup(service => service.GetLectureByIdAsync(1, It.IsAny<string>()))
                .ReturnsAsync(lecture);

             var result = await _controller.Edit(1, lecture);

             _mockLectureService.Verify(service => service.EditLectureAsync(It.Is<Lecture>(l => l.Title == "Updated Lecture")), Times.Once);
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult, "Edit should redirect to Index on success");
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public async Task Edit_LectureNotFound_ReturnsNotFound()
        {
             _mockLectureService
                .Setup(service => service.GetLectureByIdAsync(1, It.IsAny<string>()))
                .ReturnsAsync((Lecture)null);

             var result = await _controller.Edit(1, new Lecture { Id = 1 });

             Assert.IsInstanceOf<NotFoundResult>(result, "Edit should return NotFound for a missing lecture");
        }

        [Test]
        public async Task DeleteConfirmed_DeletesLectureAndRedirects()
        {
             var lectureId = 1;

             var result = await _controller.DeleteConfirmed(lectureId);

             _mockLectureService.Verify(service => service.DeleteLectureAsync(lectureId, It.IsAny<string>()), Times.Once);
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult, "DeleteConfirmed should redirect to Index on success");
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
