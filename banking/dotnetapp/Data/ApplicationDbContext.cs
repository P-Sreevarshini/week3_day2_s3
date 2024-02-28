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
        public DbSet<Transaction> Transactions { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Schedule>()
        //           .HasOne(s => s.Team1)
        //           .WithMany()
        //           .HasForeignKey(s => s.Team1Id)
        //           .OnDelete(DeleteBehavior.Restrict); // Use Restrict for one and NoAction for others

        //     modelBuilder.Entity<Schedule>()
        //         .HasOne(s => s.Team2)
        //         .WithMany()
        //         .HasForeignKey(s => s.Team2Id);    // Configure other relationships
         
        //     base.OnModelCreating(modelBuilder);
        // }


}
}