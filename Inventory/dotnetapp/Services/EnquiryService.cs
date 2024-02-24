using dotnetapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public interface EnquiryService
    {
        Task<IEnumerable<Enquiry>> GetAllEnquiries();
        Task<Enquiry> GetEnquiryById(int id);
        Task CreateEnquiry(Enquiry enquiry);
        Task UpdateEnquiry(Enquiry enquiry);
        Task DeleteEnquiry(int EnquiryID);
    }
}
