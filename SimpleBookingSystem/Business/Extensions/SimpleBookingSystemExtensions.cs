using SimpleBookingSystem.Business.Models;
using SimpleBookingSystem.Entities;

namespace SimpleBookingSystem.Business.Extensions
{
    public static class MyExtensions
    {
        public static ResourceDTO ToResourceDTO(this Resource resource)
        {
            return new ResourceDTO
            {
                Id = resource.Id,
                Name = resource.Name,
                Quantity = resource.Quantity
            };
        }
    }
}
