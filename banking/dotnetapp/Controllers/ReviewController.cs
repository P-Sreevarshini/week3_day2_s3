// ReviewController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.AspNetCore.Authorization;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [Authorize]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviews();
            return Ok(reviews);
        }
        [Authorize]


        [HttpGet("{reviewId}")]
        public async Task<ActionResult<Review>> GetReviewById(long reviewId)
        {
            var review = await _reviewService.GetReviewById(reviewId);

            if (review == null)
            {
                return NotFound(new { message = "Cannot find any review" });
            }

            return Ok(review);
        }
        [Authorize(Roles = "Customer")]

        [HttpPost]
        public async Task<ActionResult> AddReview([FromBody] Review review)
        {
            try
            {
                var success = await _reviewService.AddReview(review);

                if (success)
                {
                    return Ok(new { message = "Review added successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add review" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Implement other CRUD methods as needed
    }
}
