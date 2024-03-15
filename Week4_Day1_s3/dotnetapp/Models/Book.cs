using System;

namespace dotnetapp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}