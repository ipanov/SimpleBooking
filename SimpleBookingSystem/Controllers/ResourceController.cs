using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.DataAccess;
using SimpleBookingSystem.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly SimpleBookingSystemDbContext _context;
        private readonly ILogger<ResourceController> _logger;

        public ResourceController(ILogger<ResourceController> logger, SimpleBookingSystemDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Resource>> Get()
        {
            var resources = await _context.Resources.ToListAsync(); 
            return Ok(resources);
        }
    }
}
