using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviewsAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving reviews: {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(long userId)
        {
            try
            {
                var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving reviews for user ID {userId}: {ex.Message}");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
public async Task<IActionResult> AddReview([FromBody] Review review)
{
    if (review == null)
    {
        return BadRequest("Review data is null");
    }

    try
    {
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("User not authenticated");
        }

        var userId = long.Parse(userIdClaim.Value);
        review.UserId = userId;

        var addedReview = await _reviewService.AddReviewAsync(review);
        return Ok(new { Message = "Review added successfully", Review = addedReview });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred while adding a review: {ex.Message}");
    }
}


       [Authorize(Roles = "Customer")]
[HttpDelete("{userId}/{reviewId}")]
public async Task<IActionResult> DeleteReview(long userId, long reviewId)
{
    try
    {
        // Check if the user is authorized to delete the review
        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !userIdClaim.Value.Equals(userId.ToString()))
        {
            return Forbid("User is not authorized to delete this review");
        }

        await _reviewService.DeleteReviewAsync(reviewId, userId);
        return Ok("Review deleted successfully");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred while deleting the review: {ex.Message}");
    }
}


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest("Review data is null");
            }

            try
            {
                await _reviewService.UpdateReviewAsync(id, updatedReview);
                return Ok("Review updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the review: {ex.Message}");
            }
        }
    }
}
