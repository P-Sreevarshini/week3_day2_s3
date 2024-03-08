using dotnetapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Data    
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    
        public DbSet<User> Users { get; set; }
            public DbSet<Account> Accounts { get; set; }
            public DbSet<FDRequest> FDRequests { get; set; }
            public DbSet<FixedDeposit> FixedDeposits { get; set; }
            public DbSet<Review> Reviews { get; set; }
            public DbSet<FDAccount> FDAccounts { get; set; }


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        //     {
        //         base.OnModelCreating(modelBuilder);

        //         modelBuilder.Entity<Transaction>()
        //             .Property(t => t.Amount)
        //             .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as per your requirements
        //     }
    }
}