using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;
using WebApplication1.Data.Models;
using WebApplication1.Services.Interfaces;
using WebApplication1.Services;
using System.Collections.Generic;
using WebApplication1.Controllers.Services.Interfaces;

namespace WebApplication1.Tests.Controllers
{
    [TestFixture]
    public class CourseControllerTests
    {
        private Mock<ICourseService> _mockCourseService;
        private CourseController _controller;

        [SetUp]
        public void Setup()
        {
             _mockCourseService = new Mock<ICourseService>();

             _controller = new CourseController(_mockCourseService.Object);
        }

         [Test]
        public void Index_ReturnsViewResult_WithListOfCourses()
        {
             var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Math" },
                new Course { Id = 2, Name = "Science" }
            };

             _mockCourseService.Setup(service => service.GetAllCourses()).Returns(courses);

             var result = _controller.Index();

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as List<Course>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
            Assert.AreEqual("Math", model[0].Name);
            Assert.AreEqual("Science", model[1].Name);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public void Create_ReturnsViewResult()
        {
             var result = _controller.Create();

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

         [Test]
        public void Create_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
             var course = new Course { Id = 1, Name = "Math" };

             var result = _controller.Create(course);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public void Create_Post_ReturnsView_WhenModelIsInvalid()
        {
             var course = new Course { Id = 1, Name = "Math" };
            _controller.ModelState.AddModelError("Name", "Required");

             var result = _controller.Create(course);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(course, viewResult.Model);
        }

         [Test]
        public void Edit_ReturnsViewResult_WithCourse()
        {
             var course = new Course { Id = 1, Name = "Math" };
            _mockCourseService.Setup(service => service.GetCourseById(1)).Returns(course);

             var result = _controller.Edit(1);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Course;
            Assert.IsNotNull(model);
            Assert.AreEqual(course, model);
        }

        [Test]
        public void Edit_ReturnsNotFound_WhenCourseDoesNotExist()
        {
             _mockCourseService.Setup(service => service.GetCourseById(1)).Returns((Course)null);

             var result = _controller.Edit(1);

             Assert.IsInstanceOf<NotFoundResult>(result);
        }

         [Test]
        public void Edit_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
             var course = new Course { Id = 1, Name = "Math" };

             var result = _controller.Edit(course);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public void Edit_Post_ReturnsView_WhenModelIsInvalid()
        {
             var course = new Course { Id = 1, Name = "Math" };
            _controller.ModelState.AddModelError("Name", "Required");

             var result = _controller.Edit(course);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(course, viewResult.Model);
        }

         [Test]
        public void Delete_ReturnsViewResult_WithCourse()
        {
             var course = new Course { Id = 1, Name = "Math" };
            _mockCourseService.Setup(service => service.GetCourseById(1)).Returns(course);

             var result = _controller.Delete(1);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Course;
            Assert.IsNotNull(model);
            Assert.AreEqual(course, model);
        }

        [Test]
        public void Delete_ReturnsNotFound_WhenCourseDoesNotExist()
        {
             _mockCourseService.Setup(service => service.GetCourseById(1)).Returns((Course)null);

             var result = _controller.Delete(1);

             Assert.IsInstanceOf<NotFoundResult>(result);
        }

         [Test]
        public void DeleteConfirmed_ReturnsRedirectToAction_WhenCourseIsDeleted()
        {
             _mockCourseService.Setup(service => service.DeleteCourse(1)).Returns(true);

             var result = _controller.DeleteConfirmed(1);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [Test]
        public void DeleteConfirmed_ReturnsNotFound_WhenCourseDeletionFails()
        {
             _mockCourseService.Setup(service => service.DeleteCourse(1)).Returns(false);

             var result = _controller.DeleteConfirmed(1);

             Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
