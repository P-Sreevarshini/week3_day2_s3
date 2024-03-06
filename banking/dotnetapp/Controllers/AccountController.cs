using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.AspNetCore.Authorization;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();
           
            return Ok(accounts);
        }
        
        [Authorize]


        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountsByUserId(long userId)
        {
            try
            {
                // Retrieve accounts associated with the specified user ID
                var accounts = await _accountService.GetAccountsByUserId(userId);

                if (accounts == null || !accounts.Any())
                {
                    return NotFound(new { message = "No accounts found for the specified user ID" });
                }

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Customer")]

        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount([FromBody] Account account)
        {
            try
            {
                var addedAccount = await _accountService.AddAccount(account);

                if (addedAccount != null)
                {
                    return Ok(addedAccount); // Return the added account
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add account" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Customer")]

        [HttpPut("{accountId}")]
        public async Task<ActionResult> UpdateAccount(long accountId, [FromBody] Account account)
        {
            try
            {
                var success = await _accountService.UpdateAccount(accountId, account);

                if (success)
                {
                    return Ok(new { message = "Account updated successfully" });
                }
                else
                {
                    return NotFound(new { message = "Cannot find any account" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Customer")]
      [HttpDelete("{userId}/{accountId}")]
        public async Task<ActionResult> DeleteAccountForUser(long userId, long accountId)
        {
            try
            {
                var success = await _accountService.DeleteAccountForUser(userId, accountId);

                if (success)
                {
                    return Ok(new { message = "Account deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Account not found for the user" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
