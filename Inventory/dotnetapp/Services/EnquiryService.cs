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

//         public async Task UpdateEnquiry(Enquiry enquiry)
// {
//     try
//     {
//         _context.Enquiries.Update(enquiry);
//         await _context.SaveChangesAsync();
//     }
//     catch (DbUpdateException ex)
//     {
//         // Log the exception
//         Console.WriteLine($"Error updating enquiry: {ex.Message}");
//         throw new Exception("Error updating enquiry. Please try again later.");
//     }
// }
public async Task UpdateEnquiry(Enquiry enquiry)
{
    var existingEnquiry = await _context.Enquiries.FindAsync(enquiry.EnquiryID);
    if (existingEnquiry != null)
    {
        // Update the properties of the existing enquiry with the values of the updated enquiry
        existingEnquiry.Title = enquiry.Title;
        existingEnquiry.Description = enquiry.Description;
        existingEnquiry.EmailID = enquiry.EmailID;
        existingEnquiry.EnquiryType = enquiry.EnquiryType;
        existingEnquiry.CourseID = enquiry.CourseID;
        existingEnquiry.UserId = enquiry.UserId;

        await _context.SaveChangesAsync(); // Save changes to the database
    }
}



public async Task DeleteEnquiry(int enquiryId)
{
    try
    {
        var enquiryToRemove = await _context.Enquiries.FindAsync(enquiryId);
        if (enquiryToRemove != null)
        {
            _context.Enquiries.Remove(enquiryToRemove);
            await _context.SaveChangesAsync();
        }
    }
    catch (DbUpdateException ex)
    {
        // Log the exception
        Console.WriteLine($"Error deleting enquiry: {ex.Message}");
        throw new Exception("Error deleting enquiry. Please try again later.");
    }
}

        public async Task<List<Enquiry>> GetEnquiriesByUserId(long userId)
        {
            return await _context.Enquiries.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
