// ReviewService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            return await _context.Reviews
            .Include(review => review.User)
            .ToListAsync();
        }

      public async Task<IEnumerable<Review>> GetReviewsByUserId(long userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .Include(review => review.User)
                .ToListAsync();
        }

        public async Task<bool> AddReview(Review review)
        {
            try
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
