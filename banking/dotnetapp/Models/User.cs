using dotnetapp.Services;

namespace dotnetapp.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string MobileNumber { get; set; }
        public string UserRole { get; set; }

        // Navigation Property: User can have multiple reviews
        //public List<Review> Reviews { get; set; }

    }
}
