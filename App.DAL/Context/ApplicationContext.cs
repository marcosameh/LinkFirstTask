using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace App.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for OrderStatus
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Name = "Pending" },
                new OrderStatus { Id = 2, Name = "Submitted" },
                new OrderStatus { Id = 3, Name = "Shipped" },
                new OrderStatus { Id = 4, Name = "Delivered" }
            );

            // Configure Order
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)               
                .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<OrderItem>()
              .Property(o => o.UnitPrice)
              .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerName)               
                .HasMaxLength(100);

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerPhone)                
                .HasMaxLength(11);

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerAddress)
                .HasMaxLength(250);

            // Configure Product
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Photo)
                .HasMaxLength(100);

            // Configure OrderStatus
            modelBuilder.Entity<OrderStatus>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}