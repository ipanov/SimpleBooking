using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimpleBookingSystem.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Metadata;

namespace SimpleBookingSystem.DataAccess
{
    public class SimpleBookingSystemDbContext : DbContext
    {
        public SimpleBookingSystemDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Resource>().HasMany(b => b.Bookings).WithOne();
            modelBuilder.Entity<Booking>().HasOne(b => b.Resource).WithMany(r => r.Bookings).HasForeignKey(b => b.ResourceId);
            modelBuilder.Entity<Resource>().HasData(CreateResources());
            modelBuilder.Entity<Booking>().HasData(CreateBookings());
            modelBuilder.Entity<Resource>()
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();
            modelBuilder.Entity<Booking>()
             .Property(p => p.Id)
             .ValueGeneratedOnAdd();
        }

        private static IEnumerable<Booking> CreateBookings()
        {
            var currentDateTime = DateTime.Now;

            return Enumerable.Range(1, 3).Select(index => new Booking
            {
                Id = index,
                DateFrom = GetQuarterHourRoundedDateTime(currentDateTime, 2),
                DateTo = GetQuarterHourRoundedDateTime(currentDateTime, 2 + index),
                BookedQuantity = 2 * index,
                ResourceId = index
            })
           .ToArray();
        }


        private static IEnumerable<Resource> CreateResources()
        {
            return Enumerable.Range(1, 3).Select(index => new Resource
            {
                Id = index,
                Name = $"Resource{index}",
                Quantity = 5 * index
            })
           .ToArray();
        }

        private static DateTime GetQuarterHourRoundedDateTime(DateTime dateTime, int i)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours(i).AddMinutes(15 * i);
        }
    }
}
