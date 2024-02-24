// using dotnetapp.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace dotnetapp.Repository
// {
//     public class EnquiryRepo
//     {
//         private readonly ApplicationDbContext _context;

//         public EnquiryRepo(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
//         {
//                         return await _context.Enquiries
//                          .Include(b => b.User) 
//                          .Include(c => c.Course) 
//                          .ToListAsync();       
//         }             

//         public async Task<Enquiry> GetEnquiryById(int id)
//         {
//             return await _context.Enquiries.FindAsync(id);
//         }

//         public async Task CreateEnquiry(Enquiry enquiry)
//         {
//             _context.Enquiries.Add(enquiry);
//             await _context.SaveChangesAsync();
//               await _context.Entry(enquiry)
//         .Reference(e => e.Course)
//         .LoadAsync();

//     await _context.Entry(enquiry)
//         .Reference(e => e.User)
//         .LoadAsync();
//         }

//         public async Task UpdateEnquiry(Enquiry enquiry)
//         {
//             _context.Entry(enquiry).State = EntityState.Modified;
//             await _context.SaveChangesAsync();
//         }

//         // EnquiryRepo.cs
//         public async Task DeleteEnquiry(int id)
//         {
//             var enquiry = await _context.Enquiries.FindAsync(id);
//             if (enquiry != null)
//             {
//                 _context.Enquiries.Remove(enquiry);
//                 await _context.SaveChangesAsync();
//             }
//         }

//     }
// }
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Repository
{
    public class EnquiryRepo
    {
        private readonly ApplicationDbContext _context;

        public EnquiryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
        {
            // Eager loading for related entities
            return await _context.Enquiries
                .Include(b => b.User)
                .Include(c => c.Course)
                .ToListAsync();
        }

        // public async Task<Enquiry> GetEnquiryById(int id)
        // {
        //         return await _context.Enquiries
        //         .Include(b => b.User)
        //         .Include(c => c.Course)
        //         .FindAsync();        }
        public async Task<Enquiry> GetEnquiryById(int id)
        {
            return await _context.Enquiries
                .Include(b => b.User)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(e => e.EnquiryID == id);
        }


        public async Task CreateEnquiry(Enquiry enquiry)
        {
            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();

            await _context.Entry(enquiry)
                .Reference(e => e.Course)
                .LoadAsync();

            await _context.Entry(enquiry)
                .Reference(e => e.User)
                .LoadAsync();
        }

      public async Task UpdateEnquiry(Enquiry enquiry)
{
    try
    {
        // Retrieve the existing entity from the database
        var existingEnquiry = await _context.Enquiries.FindAsync(enquiry.EnquiryID);
        if (existingEnquiry == null)
        {
            throw new Exception("Enquiry not found.");
        }

        // Update the properties of the existing entity
        _context.Entry(existingEnquiry).CurrentValues.SetValues(enquiry);

        // Save the changes
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        throw new Exception("Failed to update enquiry.", ex);
    }
}



        public async Task DeleteEnquiry(int id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry != null)
            {
                _context.Enquiries.Remove(enquiry);
                await _context.SaveChangesAsync();
            }
        }
    }
}