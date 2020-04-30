using DutchTreat.Entities;
using DutchTreat.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DutchTreat.Data
{
    public class DutchTreatDBContext : IdentityDbContext<StoreUser>
    {
        public DutchTreatDBContext(DbContextOptions<DutchTreatDBContext>options)
            :base(options)
        {

        }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
            .HasData(new Order()
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderNumber = "12345"
            });
        }
    }
}
