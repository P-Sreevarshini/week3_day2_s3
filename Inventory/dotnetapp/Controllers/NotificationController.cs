using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _reviewService;

        public NotificationController(NotificationService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "InventoryManager")]
        [HttpPost]
        public IActionResult AddReview([FromBody] Notification review)
        {
            if (review == null)
            {
                return BadRequest("Invalid review data");
            }

            _reviewService.AddReview(review);

            return Ok(new { Status = "Success", message = "Review added successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var reviews = _reviewService.GetAllReviews();

            return Ok(reviews);
        }
    }
}
