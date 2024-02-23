using dotnetapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers{ get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    //public DbSet<Booking> Bookings { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the relationship between Order and OrderItem
        //modelBuilder.Entity<Order>()
        //    .HasMany(o => o.OrderItems)
        //    .WithOne(oi => oi.Order)
        //    .HasForeignKey(oi => oi.OrderID);
        modelBuilder.Entity<Order>()
        .HasOne(o => o.Customer)
        .WithMany(c => c.Orders)
        .HasForeignKey(o => o.CustomerID)
        .IsRequired();

        // Define the relationship between Product and OrderItem
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithMany(o => o.Products)
            .UsingEntity<OrderItem>(
                j => j
                    .HasOne(oi => oi.Order)
                    .WithMany()
                    .HasForeignKey(oi => oi.OrderID),
                j => j
                    .HasOne(oi => oi.Product)
                    .WithMany()
                    .HasForeignKey(oi => oi.ProductID),
                j =>
                {
                    j.Property(oi => oi.Quantity);
                    // Additional properties if needed
                });

        //modelBuilder.Entity<Booking>()
        //        .HasOne(b => b.Product)
        //        .WithMany(p => p.Bookings)
        //        .HasForeignKey(b => b.ProductID);

        //    modelBuilder.Entity<Booking>()
        //        .HasOne(b => b.Customer)
        //        .WithMany(c => c.Bookings)
        //        .HasForeignKey(b => b.CustomerID);
        // Additional configurations for your entities...

        base.OnModelCreating(modelBuilder);
    }


}