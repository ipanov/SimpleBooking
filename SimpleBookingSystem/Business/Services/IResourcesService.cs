using SimpleBookingSystem.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBookingSystem.Business.Services
{
    public interface IResourcesService
    {
        public Task<IEnumerable<ResourceDTO>> GetResources();
    }
}
