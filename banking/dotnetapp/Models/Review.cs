// Review.cs
using System;

namespace dotnetapp.Models
{
    public class Review
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; } // Foreign key referencing User who wrote the review
        public string ReviewText { get; set; }
        public DateTime DatePosted { get; set; }
        public int Rating { get; set; } // Rating on a scale of 1 to 5
        // Additional properties as needed

        // Navigation property
        public User? User { get; set; }
    }
}
