using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FDAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FDAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FDAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FDAccount>>> GetFDAccounts()
        {
            return await _context.FDAccounts.ToListAsync();
        }

        // GET: api/FDAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FDAccount>> GetFDAccount(long id)
        {
            var fdAccount = await _context.FDAccounts.FindAsync(id);

            if (fdAccount == null)
            {
                return NotFound();
            }

            return fdAccount;
        }

        // POST: api/FDAccounts
        [HttpPost]
        public async Task<ActionResult<FDAccount>> PostFDAccount(FDAccount fdAccount)
        {
            _context.FDAccounts.Add(fdAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFDAccount), new { id = fdAccount.FDAccountId }, fdAccount);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFDAccount(long id)
        {
            var fdAccount = await _context.FDAccounts.FindAsync(id);
            if (fdAccount == null)
            {
                return NotFound();
            }

            _context.FDAccounts.Remove(fdAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FDAccountExists(long id)
        {
            return _context.FDAccounts.Any(e => e.FDAccountId == id);
        }
    }
}
