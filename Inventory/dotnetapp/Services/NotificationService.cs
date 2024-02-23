using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        // Exclude the Password property
    }
    public class NotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void AddReview(Notification review)
        {
            _context.Notifications.Add(review);
            _context.SaveChanges();
        }

        public List<Notification> GetAllReviews()
        {
            var reviewsWithUsers = _context.Notifications
            .Include(r => r.User)
            .Select(r => new Notification
            {
                NotificationId = r.NotificationId,
                Message = r.Message,
                ProductName = r.ProductName,
                Quantity = r.Quantity,
                DateCreated = r.DateCreated,
                User = new User
                {
                    UserId = r.User.UserId,
                    Email = r.User.Email,
                    Username = r.User.Username,
                    MobileNumber = r.User.MobileNumber
                }
            })
            .ToList();

            return reviewsWithUsers;
        }

        
    }
}
