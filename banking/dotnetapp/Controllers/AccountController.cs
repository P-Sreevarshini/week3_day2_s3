// AccountController.cs (Controller)
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetAccountById(long accountId)
        {
            var account = await _accountService.GetAccountById(accountId);

            if (account == null)
            {
                return NotFound(new { message = "Cannot find any account" });
            }

            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult> AddAccount([FromBody] Account account)
        {
            try
            {
                var success = await _accountService.AddAccount(account);

                if (success)
                {
                    return Ok(new { message = "Account added successfully" });
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

        [HttpDelete("{accountId}")]
        public async Task<ActionResult> DeleteAccount(long accountId)
        {
            try
            {
                var success = await _accountService.DeleteAccount(accountId);

                if (success)
                {
                    return Ok(new { message = "Account deleted successfully" });
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
    }
}
