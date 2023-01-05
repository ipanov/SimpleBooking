using SimpleBookingSystem.Business.Models;
using System.Threading.Tasks;

namespace SimpleBookingSystem.Business.Services
{
    public interface IBookingService
    {
        public Task<bool> SaveBooking(BookingDTO bookingData);
    }
}
