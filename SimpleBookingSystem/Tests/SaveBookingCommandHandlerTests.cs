using Moq;
using SimpleBookingSystem.Business.Commands;
using SimpleBookingSystem.Business.Handlers;
using SimpleBookingSystem.Business.Models;
using SimpleBookingSystem.Business.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SimpleBookingSystem.Tests
{
    public class SaveBookingCommandHandlerTests
    {
        [Fact]
        public async Task SaveBookingCommandHandler_Sucess()
        {
            //Arrange
            var bookingServiceMock = new Mock<IBookingService>();
            bookingServiceMock.Setup(p => p.SaveBooking(It.IsAny<BookingDTO>())).ReturnsAsync(true);

            var saveBookingCommand = new SaveBookingCommand
            {
                BookingData = new BookingDTO
                {
                    ResourceId = 1,
                    BookedQuantity = 1,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now
                },
                CreatedByUserId = 1
            };

            var cancellationToken = new CancellationToken();


            var saveBookingCommandHandler = new SaveBookingCommandHandler(bookingServiceMock.Object);
            //Act
            var result = await saveBookingCommandHandler.Handle(saveBookingCommand, cancellationToken);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveBookingCommandHandler_SaveBooking_ExceptionAsync()
        {
            //Arrange
            var bookingServiceMock = new Mock<IBookingService>();
            bookingServiceMock.Setup(p => p.SaveBooking(It.IsAny<BookingDTO>())).Throws(It.IsAny<NullReferenceException>);

            var saveBookingCommand = new SaveBookingCommand
            {
                BookingData = new BookingDTO
                {
                    ResourceId = 1,
                    BookedQuantity = 10,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now
                },
                CreatedByUserId = 1
            };

            var cancellationToken = new CancellationToken();

            var saveBookingCommandHandler = new SaveBookingCommandHandler(bookingServiceMock.Object);

            await Assert.ThrowsAsync<NullReferenceException>(() => saveBookingCommandHandler.Handle(saveBookingCommand, cancellationToken));
        }
    }
}
