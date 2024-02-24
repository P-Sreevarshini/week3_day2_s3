using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using dotnetapp.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnquiryController : ControllerBase
    {
        private readonly EnquiryService _enquiryService;
        private readonly UserService _userService;

        public EnquiryController(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }
//  [Authorize(Roles="Admin,Student")]

        [HttpGet]
        public async Task<IActionResult> GetAllEnquiries()
        {
            var enquiries = await _enquiryService.GetAllEnquiries();
            return Ok(enquiries);
        }
// [Authorize(Roles="Admin,Student")]

        [HttpGet("{EnquiryID}")]
        public async Task<IActionResult> GetEnquiryById(int EnquiryID)
        {
            var enquiry = await _enquiryService.GetEnquiryById(EnquiryID);
            if (enquiry == null)
            {
                return NotFound();
            }
            return Ok(enquiry);
        }

    // [Authorize(Roles="Student")]
    [HttpPost]
    public async Task<IActionResult> CreateEnquiry(Enquiry enquiry)
    {
        await _enquiryService.CreateEnquiry(enquiry);
        return CreatedAtAction(nameof(GetEnquiryById), new { EnquiryID = enquiry.EnquiryID }, enquiry);
    }
        // public async Task<IActionResult> CreateEnquiry(Enquiry enquiry)
        // {
        //     // Retrieve the user by email
        //     var user = await _userService.GetUserByEmail(enquiry.EmailID);

        //     // Check if the user exists
        //     if (user == null)
        //     {
        //         return BadRequest("User with the provided email does not exist.");
        //     }

        //     // Associate the UserId with the Enquiry
        //     enquiry.UserId = user.UserId;

        //     // Create the enquiry
        //     await _enquiryService.CreateEnquiry(enquiry);

        //     return CreatedAtAction(nameof(GetEnquiryById), new { EnquiryID = enquiry.EnquiryID }, enquiry);
        // }

        // [Authorize(Roles="Student")]

        // [HttpPut("{EnquiryID}")]
        // public async Task<IActionResult> UpdateEnquiry(int EnquiryID, Enquiry enquiry)
        // {
        //     if (EnquiryID != enquiry.EnquiryID)
        //     {
        //         return BadRequest();
        //     }

        //     var existingEnquiry = await _enquiryService.GetEnquiryById(EnquiryID);
        //     if (existingEnquiry == null)
        //     {
        //         return NotFound("Enquiry is not found");
        //     }

        //     try
        //     {
        //         await _enquiryService.UpdateEnquiry(enquiry);
        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(500);
        //     }

        //     return NoContent();
        // }
[HttpPut("{EnquiryID}")]
public async Task<IActionResult> UpdateEnquiry(int EnquiryID, Enquiry enquiry)
{
    if (EnquiryID != enquiry.EnquiryID)
    {
        return BadRequest("EnquiryID in the request body does not match the route parameter.");
    }

    try
    {
        var existingEnquiry = await _enquiryService.GetEnquiryById(EnquiryID);
        if (existingEnquiry == null)
        {
            return NotFound("Enquiry not found.");
        }

        await _enquiryService.UpdateEnquiry(enquiry);

        return NoContent();
    }
    catch (Exception ex)
    {
        // Log the exception
        Console.WriteLine(ex);

        return StatusCode(500, "An unexpected error occurred while updating the enquiry.");
    }
}

        // [Authorize(Roles="Student")]

       [HttpDelete("{EnquiryID}")]
        public async Task<IActionResult> DeleteEnquiry(int EnquiryID)
        {
            await _enquiryService.DeleteEnquiry(EnquiryID);
            return NoContent();
        }


    }
}