using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Business.Models;
using SimpleBookingSystem.Business.Services;
using SimpleBookingSystem.DataAccessLayer;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingSystem.Business.Validators
{
    public class BookingValidator : IBookingValidator
    {
        private readonly SimpleBookingSystemDbContext _context;

        public BookingValidator(SimpleBookingSystemDbContext context, ILogger<BookingService> logger)
        {
            _context = context;
        }

        public async Task<bool> BookingIsValid(BookingDTO bookingRequest)
        {
            var resource = await _context.Resources.SingleOrDefaultAsync(r => r.Id == bookingRequest.ResourceId);

            var overlapingBookings =
              _context.Bookings
                 .Where(b => b.ResourceId == bookingRequest.ResourceId
                 && (bookingRequest.DateTo >= b.DateFrom && b.DateTo >= bookingRequest.DateFrom)).Sum(b => b.BookedQuantity);

            return (overlapingBookings + bookingRequest.BookedQuantity) > resource.Quantity ? false : true;
        }
    }
}
