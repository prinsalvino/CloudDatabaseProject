using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class ShoppingContext : DbContext
    {
        public DbSet<User> Users {  get; set;}

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Image>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
