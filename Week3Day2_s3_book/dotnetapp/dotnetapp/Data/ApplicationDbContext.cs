using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction" },
                new Category { Id = 2, Name = "Non-Fiction" },
                new Category { Id = 3, Name = "Science" }
            );

            // Seed books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Book Title 1",
                    Author = "Author 1",
                    Price = 19.99m,
                    Description = "Description for Book 1",
                    CoverImage = "book1.jpg",
                    CategoryId = 1 // Fiction
                },
                new Book
                {
                    Id = 2,
                    Title = "Book Title 2",
                    Author = "Author 2",
                    Price = 24.99m,
                    Description = "Description for Book 2",
                    CoverImage = "book2.jpg",
                    CategoryId = 2 // Non-Fiction
                },
                new Book
                {
                    Id = 3,
                    Title = "Book Title 3",
                    Author = "Author 3",
                    Price = 29.99m,
                    Description = "Description for Book 3",
                    CoverImage = "book3.jpg",
                    CategoryId = 3 // Science
                }
            );

            modelBuilder.Entity<ShoppingCartItem>().HasData(new ShoppingCartItem
            {
                Id = 1,
                BookId = 1,
                Quantity = 1,
            },
            new ShoppingCartItem
            {
                Id = 2,
                BookId = 2,
                Quantity = 2,
            }
            );

            // You can also seed shopping carts and shopping cart items if needed.

        }
    }
}