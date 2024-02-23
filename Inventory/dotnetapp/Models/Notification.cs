namespace dotnetapp.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

        // Navigation Property: Review belongs to one user
        public User? User { get; set; }
    }
}
