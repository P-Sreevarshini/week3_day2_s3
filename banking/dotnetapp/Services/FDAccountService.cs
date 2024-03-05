using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class FDAccountService
    {
        private readonly ApplicationDbContext _context;

        public FDAccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all FDAccounts
        public async Task<List<FDAccount>> GetAllFDAccountsAsync()
        {
            return await _context.FDAccounts.ToListAsync();
        }

        // Get FDAccount by ID
        public async Task<FDAccount> GetFDAccountByIdAsync(long id)
        {
            return await _context.FDAccounts.FindAsync(id);
        }

        public async Task<FDAccount> AddFDAccountAsync(FDAccount fdAccount)
        {
            _context.FDAccounts.Add(fdAccount);
            await _context.SaveChangesAsync();
            return fdAccount;
        }

        // Update FDAccount
        public async Task<FDAccount> UpdateFDAccountAsync(long id, FDAccount fdAccount)
        {
            if (id != fdAccount.FDAccountId)
            {
                throw new ArgumentException("Invalid ID");
            }

            _context.Entry(fdAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FDAccountExists(id))
                {
                    throw new KeyNotFoundException($"FDAccount with ID {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return fdAccount;
        }

        // Delete FDAccount
        public async Task<FDAccount> DeleteFDAccountAsync(long id)
        {
            var fdAccount = await _context.FDAccounts.FindAsync(id);
            if (fdAccount == null)
            {
                throw new KeyNotFoundException($"FDAccount with ID {id} not found");
            }

            _context.FDAccounts.Remove(fdAccount);
            await _context.SaveChangesAsync();

            return fdAccount;
        }

        // Check if FDAccount exists
        private bool FDAccountExists(long id)
        {
            return _context.FDAccounts.Any(e => e.FDAccountId == id);
        }
    }
}
