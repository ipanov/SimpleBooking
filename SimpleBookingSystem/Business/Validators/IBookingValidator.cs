using SimpleBookingSystem.Business.Models;
using System.Threading.Tasks;

namespace SimpleBookingSystem.Business.Validators
{
    public interface IBookingValidator
    {
       public Task<bool> BookingIsValid(BookingDTO bookingData);
    }
}
