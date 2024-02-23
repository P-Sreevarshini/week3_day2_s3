using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class EnquiryController : ControllerBase
    {
        private readonly EnquiryService _enquiryService;

        public EnquiryController(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }
 [Authorize(Roles="Admin")]

        [HttpGet("enquiry")]
        public async Task<IActionResult> GetAllEnquiries()
        {
            var enquiries = await _enquiryService.GetAllEnquiries();
            return Ok(enquiries);
        }
[Authorize(Roles="Admin,Student")]

        [HttpGet("enquiry/{id}")]
        public async Task<IActionResult> GetEnquiryById(int id)
        {
            var enquiry = await _enquiryService.GetEnquiryById(id);
            if (enquiry == null)
            {
                return NotFound();
            }
            return Ok(enquiry);
        }
        [Authorize(Roles = "Admin,Student")]
        [HttpGet("user/{userId}")] // New endpoint to get enquiries by user ID
        public async Task<IActionResult> GetEnquiriesByUserId(long userId)
        {
            var enquiries = await _enquiryService.GetEnquiriesByUserId(userId);
            if (enquiries == null || enquiries.Count == 0)
            {
                return NotFound();
            }
            return Ok(enquiries);
        }

[Authorize(Roles="Student")]
[HttpPost("enquiry")]
public async Task<IActionResult> CreateEnquiry(Enquiry enquiry)
{
    await _enquiryService.CreateEnquiry(enquiry);
    return CreatedAtAction(nameof(GetEnquiriesByUserId), new { userId = enquiry.UserId }, enquiry);
}


[Authorize(Roles="Student")]

        [HttpPut("enquiry/{id}")]
        public async Task<IActionResult> UpdateEnquiry(int EnquiryID, Enquiry enquiry)
        {
            if (EnquiryID != enquiry.EnquiryID)
            {
                return BadRequest();
            }

            var existingEnquiry = await _enquiryService.GetEnquiryById(EnquiryID);
            if (existingEnquiry == null)
            {
                return NotFound();
            }

            try
            {
                await _enquiryService.UpdateEnquiry(enquiry);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

[Authorize(Roles="Student")]

       [HttpDelete("enquiry/{id}")]
        public async Task<IActionResult> DeleteEnquiry(int id)
        {
            await _enquiryService.DeleteEnquiry(id);
            return NoContent();
        }


    }
}