using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Data.Models;
using WebApplication1.Services.Interfaces;
using System.Collections.Generic;
using WebApplication1.Data.Models.Enums;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class RoomControllerTests
    {
        private Mock<IRoomService> _mockRoomService;
        private RoomController _controller;

        [SetUp]
        public void Setup()
        {
             _mockRoomService = new Mock<IRoomService>();
            _controller = new RoomController(_mockRoomService.Object);
        }

        [Test]
        public void Index_ReturnsViewResult_WithListOfRooms()
        {
             var rooms = new List<Room> { new Room(), new Room() };  
            _mockRoomService.Setup(service => service.GetAllRooms()).Returns(rooms);

             var result = _controller.Index();

             var viewResult = result as ViewResult;  
            Assert.IsNotNull(viewResult); 
            var model = viewResult.Model as List<Room>; 
            Assert.AreEqual(2, model.Count);  
        }
        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
        [Test]
        public void Create_Get_ReturnsViewResult()
        {
             var result = _controller.Create();

             Assert.IsInstanceOf<ViewResult>(result);  
        }

        [Test]
        public void Create_Post_ValidModel_ReturnsRedirectToActionResult()
        {
             var room = new Room { Id = 1, Name = "Room 101", Type = RoomType.ComputerLab };  
            _mockRoomService.Setup(service => service.AddRoom(room));  

             var result = _controller.Create(room);

             var redirectResult = result as RedirectToActionResult;  
            Assert.IsNotNull(redirectResult);  
            Assert.AreEqual("Index", redirectResult.ActionName);  
        }

        [Test]
        public void Edit_Get_RoomNotFound_ReturnsNotFoundResult()
        {
             _mockRoomService.Setup(service => service.GetRoomById(1)).Returns((Room)null);  

             var result = _controller.Edit(1);

             Assert.IsInstanceOf<NotFoundResult>(result);  
        }

        [Test]
        public void Edit_Post_ValidModel_ReturnsRedirectToActionResult()
        {
             var room = new Room { Id = 1, Name = "Room 101", Type = RoomType.Auditorium }; 
            _mockRoomService.Setup(service => service.UpdateRoom(room));  

             var result = _controller.Edit(room);

             var redirectResult = result as RedirectToActionResult; 
            Assert.IsNotNull(redirectResult);  
            Assert.AreEqual("Index", redirectResult.ActionName);  
        }

        
        
    }
}
