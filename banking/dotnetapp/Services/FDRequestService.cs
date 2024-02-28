// FDRequestService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public class FDRequestService
    {
        private readonly ApplicationDbContext _context;

        public FDRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FDRequest>> GetAllFDRequests()
        {
            return await _context.FDRequests.ToListAsync();
        }

        public async Task<FDRequest> GetFDRequestById(long requestId)
        {
            return await _context.FDRequests
                .FirstOrDefaultAsync(fr => fr.FDRequestId == requestId);
        }

        public async Task<bool> AddFDRequest(FDRequest fdRequest)
        {
            try
            {
                _context.FDRequests.Add(fdRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exception as needed
                return false;
            }
        }

        public async Task<bool> UpdateFDRequest(long requestId, FDRequest fdRequest)
        {
            try
            {
                var existingFDRequest = await _context.FDRequests.FindAsync(requestId);

                if (existingFDRequest != null)
                {
                    // Update properties of existingFDRequest with the new values from fdRequest
                    existingFDRequest.FixedDepositId = fdRequest.FixedDepositId;
                    existingFDRequest.Status = fdRequest.Status;

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // Handle exception as needed
                return false;
            }
        }

        public async Task<bool> DeleteFDRequest(long requestId)
        {
            try
            {
                var fdRequest = await _context.FDRequests.FindAsync(requestId);

                if (fdRequest != null)
                {
                    _context.FDRequests.Remove(fdRequest);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // Handle exception as needed
                return false;
            }
        }

        // Additional methods for other business logic
    }
}
