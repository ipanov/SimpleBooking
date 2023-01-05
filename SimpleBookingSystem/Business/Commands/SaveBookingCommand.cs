using MediatR;
using SimpleBookingSystem.Business.Models;

namespace SimpleBookingSystem.Business.Commands
{
    public class SaveBookingCommand : IRequest<bool>
    {
        public BookingDTO BookingData { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
