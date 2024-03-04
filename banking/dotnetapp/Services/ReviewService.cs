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
                .Include(review => review.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(long userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .Include(review => review.User)
                .ToListAsync();
        }

        // public async Task<Review> AddReviewAsync(Review review)
        // {
        //     if (review == null)
        //     {
        //         throw new ArgumentNullException(nameof(review), "Review cannot be null");
        //     }

        //     review.DateCreated = DateTime.Now;

        //     _context.Reviews.Add(review);
        //     await _context.SaveChangesAsync();

        //     return review;
        // }
        public async Task<Review> AddReviewAsync(Review review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review), "Review cannot be null");
            }

            review.DateCreated = DateTime.Now;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Fetch user details corresponding to the user ID in the review
            review.User = await _context.Users.FindAsync(review.UserId);

            return review;
        }
    }
}
