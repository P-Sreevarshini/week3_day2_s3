// AccountService.cs (Service)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _context.Accounts
                .Include(account => account.User) // Include user details
                .ToListAsync();
        }

        public async Task<Account> GetAccountById(long accountId)
        {
            return await _context.Accounts
                .Include(account => account.User)
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

      public async Task<Account> AddAccount(Account account)
        {
            try
            {
                // Check if the user with the specified UserId exists
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == account.UserId);
                
                if (existingUser == null)
                {
                    // User does not exist, return null or handle accordingly
                    return null;
                }
                
                // Associate the account with the existing user
                account.User = existingUser;
                
                // Add the account to the database
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                
                return account; // Return the added account
            }
            catch (Exception)
            {
                // Log the exception if needed
                return null;
            }
        }



        public async Task<bool> UpdateAccount(long accountId, Account account)
        {
            try
            {
                var existingAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.AccountId == accountId);

                if (existingAccount == null)
                    return false;

                existingAccount.Balance = account.Balance;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log the exception if needed
                return false;
            }
        }

        public async Task<bool> DeleteAccountForUser(long userId, long accountId)
        {
            try
            {
                // Check if the user exists
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (existingUser == null)
                {
                    return false; // User not found
                }

                // Check if the account exists and belongs to the user
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);
                if (account == null)
                {
                    return false; // Account not found for the user
                }

                // Remove the account from the database
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                return true; // Account deleted successfully
            }
            catch (Exception)
            {
                // Log the exception if needed
                return false;
            }
        }

       public async Task<IEnumerable<Account>> GetAccountsByUserId(long userId)
        {
            return await _context.Accounts
                .Include(account => account.User)
                .Where(account => account.UserId == userId)
                .ToListAsync();
        }

    }
}
