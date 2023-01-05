using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Business.Extensions;
using SimpleBookingSystem.Business.Models;
using SimpleBookingSystem.Business.Validators;
using SimpleBookingSystem.DataAccessLayer;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingSystem.Business.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly SimpleBookingSystemDbContext _context;
        private readonly ILogger<ResourcesService> _logger;

        public ResourcesService(SimpleBookingSystemDbContext context, ILogger<ResourcesService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<ResourceDTO>> GetResources()
        {
            var resources = await _context.Resources.ToListAsync();
            return resources.Select(r => r.ToResourceDTO());
        }
    }
}