// FDRequestController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [Route("api/fdrequest")]
    [ApiController]
    public class FDRequestController : ControllerBase
    {
        private readonly FDRequestService _fdRequestService;

        public FDRequestController(FDRequestService fdRequestService)
        {
            _fdRequestService = fdRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FDRequest>>> GetAllFDRequests()
        {
            var fdRequests = await _fdRequestService.GetAllFDRequests();
            return Ok(fdRequests);
        }

        [HttpGet("{requestId}")]
        public async Task<ActionResult<FDRequest>> GetFDRequestById(long requestId)
        {
            var fdRequest = await _fdRequestService.GetFDRequestById(requestId);

            if (fdRequest == null)
            {
                return NotFound(new { message = "Cannot find any FDRequest" });
            }

            return Ok(fdRequest);
        }

        [HttpPost]
        public async Task<ActionResult> AddFDRequest([FromBody] FDRequest fdRequest)
        {
            try
            {
                var success = await _fdRequestService.AddFDRequest(fdRequest);

                if (success)
                {
                    return Ok(new { message = "FDRequest added successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add FDRequest" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{requestId}")]
        public async Task<ActionResult> UpdateFDRequest(long requestId, [FromBody] FDRequest fdRequest)
        {
            try
            {
                var success = await _fdRequestService.UpdateFDRequest(requestId, fdRequest);

                if (success)
                {
                    return Ok(new { message = "FDRequest updated successfully" });
                }
                else
                {
                    return NotFound(new { message = $"FDRequest with ID {requestId} not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{requestId}")]
        public async Task<ActionResult> DeleteFDRequest(long requestId)
        {
            try
            {
                var success = await _fdRequestService.DeleteFDRequest(requestId);

                if (success)
                {
                    return Ok(new { message = "FDRequest deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = $"FDRequest with ID {requestId} not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
