using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Service;
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

        public EnquiryController(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }
//  [Authorize(Roles="Admin,Customer")]

        [HttpGet]
        public async Task<IActionResult> GetAllEnquiries()
        {
            var enquiries = await _enquiryService.GetAllEnquiries();
            return Ok(enquiries);
        }
// [Authorize(Roles="Admin,Customer")]

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

 //  [Authorize(Roles="Customer")]
[HttpPost]
public async Task<IActionResult> CreateEnquiry(Enquiry enquiry)
{
    await _enquiryService.CreateEnquiry(enquiry);
    return CreatedAtAction(nameof(GetEnquiryById), new { EnquiryID = enquiry.EnquiryID }, enquiry);
}

// [Authorize(Roles="Customer")]

        [HttpPut("{EnquiryID}")]
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

// [Authorize(Roles="Customer")]

       [HttpDelete("{EnquiryID}")]
        public async Task<IActionResult> DeleteEnquiry(int EnquiryID)
        {
            await _enquiryService.DeleteEnquiry(EnquiryID);
            return NoContent();
        }


    }
}