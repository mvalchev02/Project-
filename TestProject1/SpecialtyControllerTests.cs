using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Data.Models;
using WebApplication1.Services.Interfaces;
using System.Collections.Generic;

namespace WebApplication1.Tests.Controllers
{
    [TestFixture]
    public class SpecialtyControllerTests
    {
        private Mock<ISpecialtyService> _mockSpecialtyService;
        private SpecialtyController _controller;

        [SetUp]
        public void Setup()
        {
            _mockSpecialtyService = new Mock<ISpecialtyService>();
            _controller = new SpecialtyController(_mockSpecialtyService.Object);
        }

         [Test]
        public void Index_ReturnsViewResult_WithListOfSpecialties()
        {
             var specialties = new List<Specialty>
            {
                new Specialty { Id = 1, Name = "Specialty 1" },
                new Specialty { Id = 2, Name = "Specialty 2" }
            };

            _mockSpecialtyService.Setup(service => service.GetAllSpecialties()).Returns(specialties);

             var result = _controller.Index();

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as List<Specialty>;
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
        public void Create_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
             var specialty = new Specialty { Id = 1, Name = "Specialty 1" };

            _mockSpecialtyService.Setup(service => service.AddSpecialty(specialty));
             var result = _controller.Create(specialty);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

         [Test]
        public void Create_Post_ReturnsView_WhenModelIsInvalid()
        {
             var specialty = new Specialty { Id = 1, Name = "" };   

            _controller.ModelState.AddModelError("Name", "Required");

             var result = _controller.Create(specialty);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(specialty, viewResult.Model);
        }

         [Test]
        public void Edit_ReturnsViewResult_WithSpecialty_WhenIdIsValid()
        {
             var specialty = new Specialty { Id = 1, Name = "Specialty 1" };
            _mockSpecialtyService.Setup(service => service.GetSpecialtyById(1)).Returns(specialty);

             var result = _controller.Edit(1);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Specialty;
            Assert.IsNotNull(model);
            Assert.AreEqual(specialty, model);
        }

         [Test]
        public void Edit_ReturnsNotFound_WhenSpecialtyDoesNotExist()
        {
             _mockSpecialtyService.Setup(service => service.GetSpecialtyById(1)).Returns((Specialty)null);

             var result = _controller.Edit(1);

             Assert.IsInstanceOf<NotFoundResult>(result);
        }

         [Test]
        public void Edit_Post_ReturnsRedirectToAction_WhenModelIsValid()
        {
             var specialty = new Specialty { Id = 1, Name = "Updated Specialty" };

            _mockSpecialtyService.Setup(service => service.UpdateSpecialty(specialty));

             var result = _controller.Edit(specialty);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }

         [Test]
        public void Delete_ReturnsViewResult_WithSpecialty_WhenIdIsValid()
        {
             var specialty = new Specialty { Id = 1, Name = "Specialty 1" };
            _mockSpecialtyService.Setup(service => service.GetSpecialtyById(1)).Returns(specialty);

             var result = _controller.Delete(1);

             var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as Specialty;
            Assert.IsNotNull(model);
            Assert.AreEqual(specialty, model);
        }

         [Test]
        public void Delete_ReturnsNotFound_WhenSpecialtyDoesNotExist()
        {
             _mockSpecialtyService.Setup(service => service.GetSpecialtyById(1)).Returns((Specialty)null);

             var result = _controller.Delete(1);

             Assert.IsInstanceOf<NotFoundResult>(result);
        }

         [Test]
        public void DeleteConfirmed_ReturnsRedirectToAction_WhenSpecialtyIsDeleted()
        {
             var specialty = new Specialty { Id = 1, Name = "Specialty 1" };
            _mockSpecialtyService.Setup(service => service.GetSpecialtyById(1)).Returns(specialty);
            _mockSpecialtyService.Setup(service => service.DeleteSpecialty(1));

             var result = _controller.DeleteConfirmed(1);

             var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.ActionName);
        }
    }
}
