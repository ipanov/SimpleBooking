using MediatR;
using System.Threading.Tasks;
using System.Threading;
using SimpleBookingSystem.Business.Commands;
using System;
using SimpleBookingSystem.Business.Services;

namespace SimpleBookingSystem.Business.Handlers
{
    public class SaveBookingCommandHandler : IRequestHandler<SaveBookingCommand, bool>
    {
        private readonly IBookingService _bookingService;

        public SaveBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<bool> Handle(SaveBookingCommand command, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _bookingService.SaveBooking(command.BookingData);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }, cancellationToken);
        }
    }
}
