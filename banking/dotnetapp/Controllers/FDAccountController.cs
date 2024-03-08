using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using dotnetapp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FDAccountController : ControllerBase
    {
        private readonly FDAccountService _fdAccountService;

        public FDAccountController(FDAccountService fdAccountService)
        {
            _fdAccountService = fdAccountService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FDAccount>>> GetFDAccounts()
        {
            var fdAccounts = await _fdAccountService.GetAllFDAccountsAsync();
            return fdAccounts;
        }

        // GET: api/FDAccounts/5
                [Authorize]

        [HttpGet("{id}")]
        public async Task<ActionResult<FDAccount>> GetFDAccount(long id)
        {
            var fdAccount = await _fdAccountService.GetFDAccountByIdAsync(id);

            if (fdAccount == null)
            {
                return NotFound();
            }

            return fdAccount;
        }

        // POST: api/FDAccounts
                [Authorize]

        [HttpPost]
        public async Task<ActionResult<FDAccount>> PostFDAccount(FDAccount fdAccount)
        {
            await _fdAccountService.AddFDAccountAsync(fdAccount);

            return CreatedAtAction(nameof(GetFDAccount), new { id = fdAccount.FDAccountId }, fdAccount);
        }

        // PUT: api/FDAccounts/5
                [Authorize]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFDAccount(long id, FDAccount fdAccount)
        {
            try
            {
                await _fdAccountService.UpdateFDAccountAsync(id, fdAccount);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE: api/FDAccounts/5
                [Authorize]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFDAccount(long id)
        {
            try
            {
                await _fdAccountService.DeleteFDAccountAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
                [Authorize]

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FDAccount>>> GetFDAccountsByUserId(long userId)
        {
            var fdAccounts = await _fdAccountService.GetFDAccountsByUserIdAsync(userId);
            if (fdAccounts == null || !fdAccounts.Any())
            {
                return NotFound();
            }
            return fdAccounts;
        }


    }
}
