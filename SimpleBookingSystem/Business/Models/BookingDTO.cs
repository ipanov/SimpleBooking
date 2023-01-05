using System;

namespace SimpleBookingSystem.Business.Models
{
    public class BookingDTO
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int BookedQuantity { get; set; }

        public int ResourceId { get; set; }
    }
}
