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

        public async Task<Enquiry> GetEnquiryById(int id)
        {
            return await _context.Enquiries.FindAsync(id);
        }

        public async Task CreateEnquiry(Enquiry enquiry)
        {
            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();

            // Load related entities after saving
            await _context.Entry(enquiry)
                .Reference(e => e.Course)
                .LoadAsync();

            await _context.Entry(enquiry)
                .Reference(e => e.User)
                .LoadAsync();
        }

        public async Task UpdateEnquiry(Enquiry enquiry)
        {
            _context.Entry(enquiry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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
