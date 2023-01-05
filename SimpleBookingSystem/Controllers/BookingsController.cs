using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using SimpleBookingSystem.Business.Commands;
using SimpleBookingSystem.Entities;
using System;

namespace SimpleBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(IMediator mediator, ILogger<BookingsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post([FromBody] SaveBookingCommand saveBookingCommand)
        {
            if (saveBookingCommand.BookingData == null || saveBookingCommand.BookingData.BookedQuantity == 0)
            {
               return BadRequest("Invalid payload");
            }

            var result = await _mediator.Send(saveBookingCommand);

            if (result == false)
            {
                return BadRequest("Not enough quantity available for requested booking period");
            }

            return Ok();
        }
    }
}
