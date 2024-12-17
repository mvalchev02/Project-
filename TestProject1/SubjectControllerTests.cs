using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Data.Models;
using WebApplication1.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Tests.Controllers
{
    [TestFixture]
    public class SubjectControllerTests
    {
        private Mock<ISubjectService> _mockSubjectService;
        private SubjectController _controller;

        [SetUp]
        public void Setup()
        {
            _mockSubjectService = new Mock<ISubjectService>();
            _controller = new SubjectController(_mockSubjectService.Object);
        }

         [Test]
        public async Task Index_ReturnsViewResult_WithListOfSubjects()
        {
             var subjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "Math" },
                new Subject { Id = 2, Name = "English" }
            };

            _mockSubjectService.Setup(service => service.GetSubjectsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(subjects);

             var result = await _controller.Index("Math", 1, null);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as List<Subject>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
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

             Assert.IsInstanceOf<ViewResult>(result);
        }

         [Test]
        public async Task Create_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
             var subject = new Subject { Id = 1, Name = "Science" };

            _mockSubjectService.Setup(service => service.CreateSubjectAsync(subject)).Returns(Task.CompletedTask);

             var result = await _controller.Create(subject);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

         [Test]
        public async Task Create_Post_ReturnsView_WhenModelIsInvalid()
        {
             var subject = new Subject { Id = 1, Name = "" };   

            _controller.ModelState.AddModelError("Name", "Required");

             var result = await _controller.Create(subject);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(subject, viewResult.Model);
        }

         [Test]
        public async Task Edit_ReturnsViewResult_WithSubject_WhenIdIsValid()
        {
             var subject = new Subject { Id = 1, Name = "Math" };
            _mockSubjectService.Setup(service => service.GetSubjectByIdAsync(1)).ReturnsAsync(subject);

             var result = await _controller.Edit(1);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Subject;
            Assert.IsNotNull(model);
            Assert.AreEqual(subject, model);
        }

         [Test]
        public async Task Edit_ReturnsNotFound_WhenSubjectDoesNotExist()
        {
             _mockSubjectService.Setup(service => service.GetSubjectByIdAsync(1)).ReturnsAsync((Subject)null);

             var result = await _controller.Edit(1);

             Assert.IsInstanceOf<NotFoundResult>(result);
        }

         [Test]
        public async Task Edit_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
             var subject = new Subject { Id = 1, Name = "Updated Math" };

            _mockSubjectService.Setup(service => service.EditSubjectAsync(subject)).Returns(Task.CompletedTask);

             var result = await _controller.Edit(subject);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

          
         [Test]
        public async Task Delete_ReturnsNotFound_WhenSubjectDoesNotExist()
        {
             _mockSubjectService.Setup(service => service.GetSubjectByIdAsync(1)).ReturnsAsync((Subject)null);

             var result = await _controller.Delete(1);

             Assert.IsInstanceOf<NotFoundResult>(result);
        }

       
    }
}
