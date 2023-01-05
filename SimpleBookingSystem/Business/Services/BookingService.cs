using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Business.Emailer;
using SimpleBookingSystem.Business.Models;
using SimpleBookingSystem.Business.Validators;
using SimpleBookingSystem.DataAccessLayer;
using SimpleBookingSystem.Entities;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SimpleBookingSystem.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly SimpleBookingSystemDbContext _context;
        private readonly IBookingValidator _validator;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookingService> _logger;

        public BookingService(SimpleBookingSystemDbContext context, IBookingValidator validator, IConfiguration configuration, ILogger<BookingService> logger)
        {
            _context = context;
            _validator = validator;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<bool> SaveBooking(BookingDTO booking)
        {
            var resource = await _context.Resources.SingleOrDefaultAsync(r => r.Id == booking.ResourceId);

            var currentBookedQuantity = _context.Bookings.Where(b => b.ResourceId == booking.ResourceId && (DateTime.Compare(b.DateFrom, booking.DateFrom) >= 0 && (DateTime.Compare(b.DateTo, booking.DateTo) <= 0))).Sum(b => b.BookedQuantity);

            var isValid = await _validator.BookingIsValid(booking);

            if (!isValid)
            {
                return false;
            }

            _context.Bookings.Add(new Booking()
            {
                DateFrom = booking.DateFrom,
                DateTo = booking.DateTo,
                BookedQuantity = booking.BookedQuantity,
                Resource = resource,
                ResourceId = booking.ResourceId
            });

            _context.SaveChanges();

            var bookingId = _context.Bookings.Max(b => b.Id);

            SendBookingConfirmationEmail(bookingId);

            return true;
        }

        private void SendBookingConfirmationEmail(int id)
        {
            var from = new MailAddress("dev@simplebookingsyste.com");
            var to = new MailAddress(_configuration["Email"]);

            using (MailMessage mm = new MailMessage(from,to))
            {
                mm.Body = $"EMAIL SENT TO {to.Address} FOR CREATED BOOKING WITH ID {id}";

                using (SmtpClientWrapper smtp = new SmtpClientWrapper("", 80))
                {
                    //smtp.Host = host;
                    //smtp.EnableSsl = true;
                    //NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                    //smtp.UseDefaultCredentials = true;
                    //smtp.Credentials = NetworkCred;
                    //smtp.Port = port;
                    smtp.Send(mm);
                }
            }
        }
    }
}
