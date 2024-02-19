using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Repository
{
    public class EnquiryRepository
    {
        private readonly ApplicationDbContext _context;

        public EnquiryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
        {
            return await _context.Enquiries.ToListAsync();
        }

        public async Task<Enquiry> GetEnquiryById(int EnquiryID)
        {
            return await _context.Enquiries.FindAsync(EnquiryID);
        }

        public async Task CreateEnquiry(Enquiry enquiry)
        {
            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnquiry(Enquiry enquiry)
        {
            _context.Entry(enquiry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEnquiry(Enquiry enquiry)
        {
            _context.Enquiries.Remove(enquiry);
            await _context.SaveChangesAsync();
        }
    }
}
