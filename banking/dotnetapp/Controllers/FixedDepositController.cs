// FixedDepositController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [Route("api/fixeddeposit")]
    [ApiController]
    public class FixedDepositController : ControllerBase
    {
        private readonly FixedDepositService _fixedDepositService;

        public FixedDepositController(FixedDepositService fixedDepositService)
        {
            _fixedDepositService = fixedDepositService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FixedDeposit>>> GetAllFixedDeposits()
        {
            var fixedDeposits = await _fixedDepositService.GetAllFixedDeposits();
            return Ok(fixedDeposits);
        }

        [HttpGet("{FixedDepositId}")]
        public async Task<ActionResult<FixedDeposit>> GetFixedDepositById(long FixedDepositId)
        {
            var fixedDeposit = await _fixedDepositService.GetFixedDepositById(FixedDepositId);

            if (fixedDeposit == null)
            {
                return NotFound(new { message = "Cannot find any fixed deposit" });
            }

            return Ok(fixedDeposit);
        }

        [HttpPost]
        public async Task<ActionResult> AddFixedDeposit([FromBody] FixedDeposit fixedDeposit)
        {
            try
            {
                var success = await _fixedDepositService.AddFixedDeposit(fixedDeposit);

                if (success)
                {
                    return Ok(new { message = "Fixed deposit added successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add fixed deposit" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{FixedDepositId}")]
        public async Task<ActionResult> UpdateFixedDeposit(long FixedDepositId, [FromBody] FixedDeposit fixedDeposit)
        {
            try
            {
                var success = await _fixedDepositService.UpdateFixedDeposit(FixedDepositId, fixedDeposit);

                if (success)
                {
                    return Ok(new { message = "Fixed deposit updated successfully" });
                }
                else
                {
                    return NotFound(new { message = "Cannot find any fixed deposit" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{FixedDepositId}")]
        public async Task<ActionResult> DeleteFixedDeposit(long FixedDepositId)
        {
            try
            {
                var success = await _fixedDepositService.DeleteFixedDeposit(FixedDepositId);

                if (success)
                {
                    return Ok(new { message = "Fixed deposit deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Cannot find any fixed deposit" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
