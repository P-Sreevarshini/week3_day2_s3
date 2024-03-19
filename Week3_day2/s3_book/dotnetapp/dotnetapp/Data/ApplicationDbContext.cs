using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add some test data for books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Price = 10.99m, Description = "Description for Book 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Price = 15.99m, Description = "Description for Book 2" },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Price = 20.99m, Description = "Description for Book 3" }
            );

            // Add some test data for categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction" },
                new Category { Id = 2, Name = "Non-Fiction" },
                new Category { Id = 3, Name = "Science" }
            );

            // Add some test data for shopping cart items
            modelBuilder.Entity<ShoppingCartItem>().HasData(
                new ShoppingCartItem { Id = 1, BookId = 1, Quantity = 1 },
                new ShoppingCartItem { Id = 2, BookId = 2, Quantity = 2 }
            );
        }
    }
}
