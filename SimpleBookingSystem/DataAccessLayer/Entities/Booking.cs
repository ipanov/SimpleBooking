using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookingSystem.Entities
{
    [Table("booking")]
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public int BookedQuantity { get; set; }
        [ForeignKey("resource")]
        public int ResourceId { get; set; }
        public  Resource Resource { get; set; }
    }
}
