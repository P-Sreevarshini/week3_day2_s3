// Review.cs
using System;

namespace dotnetapp.Models
{
    public class Review
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; } 
        public string ReviewText { get; set; }
        public DateTime DatePosted { get; set; }
        public int Rating { get; set; }
        public User? User { get; set; }
    }
}
