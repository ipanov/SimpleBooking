using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.DataAccessLayer;
using SimpleBookingSystem.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly SimpleBookingSystemDbContext _context;
        private readonly ILogger<ResourcesController> _logger;

        public ResourcesController(ILogger<ResourcesController> logger, SimpleBookingSystemDbContext context)
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
