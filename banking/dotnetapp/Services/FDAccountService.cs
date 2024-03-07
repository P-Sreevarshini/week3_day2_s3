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
            var fdAccounts = await _context.FDAccounts.ToListAsync();
            foreach (var fdAccount in fdAccounts)
            {
                await PopulateUserAndFixedDeposit(fdAccount);
            }
            return fdAccounts;
        }

        // Get FDAccount by ID
        public async Task<FDAccount> GetFDAccountByIdAsync(long id)
        {
            var fdAccount = await _context.FDAccounts.FindAsync(id);
            if (fdAccount != null)
            {
                await PopulateUserAndFixedDeposit(fdAccount);
            }
            return fdAccount;
        }

        public async Task<FDAccount> AddFDAccountAsync(FDAccount fdAccount)
        {
            await PopulateUserAndFixedDeposit(fdAccount);

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

            await PopulateUserAndFixedDeposit(fdAccount);

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
        public async Task<List<FDAccount>> GetFDAccountsByUserIdAsync(long userId)
        {
            var fdAccounts = await _context.FDAccounts.Where(fd => fd.UserId == userId).ToListAsync();
            foreach (var fdAccount in fdAccounts)
            {
                await PopulateUserAndFixedDeposit(fdAccount);
            }
            return fdAccounts;
        }
        private async Task PopulateUserAndFixedDeposit(FDAccount fdAccount)
        {
            fdAccount.User = await _context.Users.FindAsync(fdAccount.UserId);
            fdAccount.FixedDeposit = await _context.FixedDeposits.FindAsync(fdAccount.FixedDepositId);
        }
    }
}
