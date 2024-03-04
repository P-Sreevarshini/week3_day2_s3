using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System;
using System.Threading.Tasks;
using System.Security.Claims;


namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly AuthService _authService; // Inject AuthService

        public ReviewController(ReviewService reviewService, AuthService authService)
        {
            _reviewService = reviewService;
            _authService = authService; // Assign injected AuthService
        }
        
        // [Authorize(Roles = "Admin")]
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

        // [Authorize]
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

        // [Authorize(Roles = "Customer")]
        // [HttpPost]
        // public async Task<IActionResult> AddReview([FromBody] Review review)
        // {
        //     if (review == null)
        //     {
        //         return BadRequest("Review data is null");
        //     }

        //     try
        //     {
        //         // Perform authentication to ensure the user is authorized to add a review
        //         var userId = long.Parse(HttpContext.User.FindFirst("userId").Value); // Assuming userId claim is present
        //         review.UserId = userId;

        //         var addedReview = await _reviewService.AddReviewAsync(review);
        //         return Ok(addedReview);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"An error occurred while adding a review: {ex.Message}");
        //     }
        // }
            // [Authorize(Roles = "Customer")]
            [HttpPost]
            public async Task<IActionResult> AddReview([FromBody] Review review)
            {
                if (review == null)
                {
                    return BadRequest("Review data is null");
                }

                try
                {
                    var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    review.UserId = userId;

                    var addedReview = await _reviewService.AddReviewAsync(review);
                    var user = await _authService.GetUserByIdAsync(review.UserId);
                    if (user == null)
                    {
                        return BadRequest("User not found");
                    }
                    var response = new
                    {
                        ReviewId = addedReview.ReviewId,
                        Body = addedReview.Body,
                        Rating = addedReview.Rating,
                        DateCreated = addedReview.DateCreated,
                        userId = addedReview.UserId, // Add user ID to the response
                        // userName = user.Username // Assuming 'Name' is the property that holds the user's name
                    };

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while adding a review: {ex.Message}");
                }
            }

    }
}
