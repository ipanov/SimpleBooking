using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.DataAccess;
using System.Net;
using System.Threading.Tasks;
using System;
using SimpleBookingSystem.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace SimpleBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly SimpleBookingSystemDbContext _context;
        private readonly ILogger<ResourcesController> _logger;

        public BookingsController(ILogger<ResourcesController> logger, SimpleBookingSystemDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post([FromBody] Booking booking)
        {
            if (booking == null || booking.BookedQuantity == 0)
            {
                return BadRequest("Invalid payload");
            }


            var resource = await _context.Resources.SingleOrDefaultAsync(r => r.Id == booking.ResourceId);

            var currentBookedQuantity = _context.Bookings.Where(b => b.ResourceId == booking.ResourceId && (DateTime.Compare(b.DateFrom, booking.DateFrom) >= 0 && (DateTime.Compare(b.DateTo, booking.DateTo) <= 0))).Sum(b => b.BookedQuantity);

            if ((currentBookedQuantity + booking.BookedQuantity) > resource.Quantity)
            {
                return BadRequest("Not enough quantity available for requested booking period");
            }


            _context.Bookings.Add(new Booking()
            {
                Id = _context.Bookings.Max(b => b.Id) + 1,
                DateFrom = booking.DateFrom,
                DateTo = booking.DateTo,
                BookedQuantity = booking.BookedQuantity,
                Resource = resource,
                ResourceId = booking.ResourceId
            });

            _context.SaveChanges();

            return Ok();
        }
    }
}
