using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

      public async Task<IEnumerable<Review>> GetAllReviewsAsync()
    {
        return await _context.Reviews
            .Include(review => review.User) // Include user details
            .ToListAsync();
    }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(long userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

public async Task<Review> AddReviewAsync(Review review)
{
    if (review == null)
    {
        throw new ArgumentNullException(nameof(review), "Review cannot be null");
    }

    review.DateCreated = DateTime.Now;

    var user = await _context.Users.FindAsync(review.UserId);
    if (user == null)
    {
        throw new InvalidOperationException($"User with ID {review.UserId} not found");
    }

    review.User = user;

    _context.Reviews.Add(review);
    await _context.SaveChangesAsync();

    return review; // Return the added review object
}


        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateReviewAsync(int id, Review updatedReview)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                review.Body = updatedReview.Body;
                review.Rating = updatedReview.Rating;
                await _context.SaveChangesAsync();
            }
        }
    }
}
