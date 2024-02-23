using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class EnquiryService
    {
        private readonly ApplicationDbContext _context;

        public EnquiryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
        {
            return await _context.Enquiries.ToListAsync();
        }

        public async Task<Enquiry> GetEnquiryById(int enquiryId)
        {
            return await _context.Enquiries.FindAsync(enquiryId);
        }

        public async Task CreateEnquiry(Enquiry enquiry)
        {
            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnquiry(Enquiry enquiry)
        {
            _context.Enquiries.Update(enquiry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEnquiry(int enquiryId)
        {
            var enquiryToRemove = await _context.Enquiries.FindAsync(enquiryId);
            if (enquiryToRemove != null)
            {
                _context.Enquiries.Remove(enquiryToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Enquiry>> GetEnquiriesByUserId(long userId)
        {
            return await _context.Enquiries.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
