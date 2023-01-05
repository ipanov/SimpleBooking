using Castle.Core.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleBookingSystem.Business.Commands;
using SimpleBookingSystem.Business.Models;
using SimpleBookingSystem.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SimpleBookingSystem.Tests
{
    public class BoookingsControllerTests
    {
        [Fact]
        public async Task Post_Ok()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<SaveBookingCommand>(), default(CancellationToken))).ReturnsAsync(true);
 
            var loggerMock = new Mock<ILogger<BookingsController>>();
       
            var controller = new BookingsController(mediatorMock.Object, loggerMock.Object);

            var saveBookingCommand = new SaveBookingCommand
            {
                CreatedByUserId = 1,
                BookingData = new BookingDTO
                {
                    ResourceId = 1,
                    BookedQuantity = 1,
                    DateFrom = DateTime.Now,
                    DateTo= DateTime.Now,   
                }

            };

            // Act
            var response = await controller.Post(saveBookingCommand);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task Post_BadRequest()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<SaveBookingCommand>(), default(CancellationToken))).ReturnsAsync(true);

            var loggerMock = new Mock<ILogger<BookingsController>>();

            var controller = new BookingsController(mediatorMock.Object, loggerMock.Object);

            var saveBookingCommand = new SaveBookingCommand
            {
                CreatedByUserId = 1,
                BookingData = new BookingDTO
                {
                    ResourceId = 1,
                    BookedQuantity = 0,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                }

            };

            // Act
            var response = await controller.Post(saveBookingCommand);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
