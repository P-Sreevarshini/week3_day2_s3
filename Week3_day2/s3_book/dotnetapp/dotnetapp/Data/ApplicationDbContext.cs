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

    // Define DbSet properties for each of your entities
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
}
}
