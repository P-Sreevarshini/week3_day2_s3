// FixedDepositService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class FixedDepositService
    {
        private readonly ApplicationDbContext _context;

        public FixedDepositService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FixedDeposit>> GetAllFixedDeposits()
        {
            return await _context.FixedDeposits.Include(fd => fd.User).ToListAsync();
        }

        public async Task<FixedDeposit> GetFixedDepositById(long FixedDepositId)
        {
            return await _context.FixedDeposits.Include(fd => fd.User)
                .FirstOrDefaultAsync(fd => fd.FixedDepositId == FixedDepositId);
        }

        public async Task<bool> AddFixedDeposit(FixedDeposit fixedDeposit)
        {
            try
            {
                _context.FixedDeposits.Add(fixedDeposit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateFixedDeposit(long FixedDepositId, FixedDeposit fixedDeposit)
        {
            try
            {
                var existingFixedDeposit = await _context.FixedDeposits
                    .FirstOrDefaultAsync(fd => fd.FixedDepositId == FixedDepositId);

                if (existingFixedDeposit == null)
                    return false;

                fixedDeposit.FixedDepositId = FixedDepositId;
                _context.Entry(existingFixedDeposit).CurrentValues.SetValues(fixedDeposit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteFixedDeposit(long FixedDepositId)
        {
            try
            {
                var fixedDeposit = await _context.FixedDeposits
                    .FirstOrDefaultAsync(fd => fd.FixedDepositId == FixedDepositId);

                if (fixedDeposit == null)
                    return false;

                _context.FixedDeposits.Remove(fixedDeposit);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
