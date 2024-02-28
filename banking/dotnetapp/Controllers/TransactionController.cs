// TransactionController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;

namespace dotnetapp.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(long transactionId)
        {
            var transaction = await _transactionService.GetTransactionById(transactionId);

            if (transaction == null)
            {
                return NotFound(new { message = "Cannot find any transaction" });
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            try
            {
                var success = await _transactionService.AddTransaction(transaction);

                if (success)
                {
                    return Ok(new { message = "Transaction added successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add transaction" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{transactionId}")]
        public async Task<ActionResult> UpdateTransaction(long transactionId, [FromBody] Transaction transaction)
        {
            try
            {
                var success = await _transactionService.UpdateTransaction(transactionId, transaction);

                if (success)
                {
                    return Ok(new { message = "Transaction updated successfully" });
                }
                else
                {
                    return NotFound(new { message = "Cannot find any transaction" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{transactionId}")]
        public async Task<ActionResult> DeleteTransaction(long transactionId)
        {
            try
            {
                var success = await _transactionService.DeleteTransaction(transactionId);

                if (success)
                {
                    return Ok(new { message = "Transaction deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Cannot find any transaction" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
