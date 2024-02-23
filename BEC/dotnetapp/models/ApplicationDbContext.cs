// using Microsoft.EntityFrameworkCore;

// namespace dotnetapp.Models
// {
//     public class ApplicationDbContext : DbContext
//     {
//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//         {
//         }

//        public DbSet<User> Users { get; set; }
//         public DbSet<Course> Courses { get; set; }
//         public DbSet<Enquiry> Enquiries { get; set; }
//         public DbSet<Payment> Payments { get; set; }
//     }
// }

using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Enquiry and User
            modelBuilder.Entity<Enquiry>()
                .HasOne(e => e.User)
                .WithMany()  // Adjust this if you have a specific navigation property on User referring back to Enquiries
                .HasForeignKey(e => e.UserId)  // Foreign key in Enquiry
                .HasPrincipalKey(u => u.UserId);  // Primary key in User
        }
    }
}
