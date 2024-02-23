using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class EnquiryService
    {
        private readonly List<Enquiry> _enquiries;

        public EnquiryService()
        {
            // Initialize an empty list of enquiries
            _enquiries = new List<Enquiry>();
        }

        public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
        {
            // Return all enquiries in the list
            return _enquiries;
        }

        public async Task<Enquiry> GetEnquiryById(int enquiryId)
        {
            // Find the enquiry with the provided ID in the list
            return _enquiries.FirstOrDefault(e => e.EnquiryID == enquiryId);
        }

        public async Task CreateEnquiry(Enquiry enquiry)
        {
            // Assign a new ID to the enquiry
            enquiry.EnquiryID = _enquiries.Count > 0 ? _enquiries.Max(e => e.EnquiryID) + 1 : 1;
            
            // Add the new enquiry to the list
            _enquiries.Add(enquiry);
        }

        public async Task UpdateEnquiry(Enquiry enquiry)
        {
            // Find the existing enquiry in the list
            var existingEnquiry = _enquiries.FirstOrDefault(e => e.EnquiryID == enquiry.EnquiryID);
            if (existingEnquiry != null)
            {
                // Update the existing enquiry's properties
                existingEnquiry.EnquiryDate = enquiry.EnquiryDate;
                existingEnquiry.Title = enquiry.Title;
                existingEnquiry.Description = enquiry.Description;
                existingEnquiry.EmailID = enquiry.EmailID;
                existingEnquiry.EnquiryType = enquiry.EnquiryType;
                existingEnquiry.CourseID = enquiry.CourseID;
                existingEnquiry.UserId = enquiry.UserId;
            }
        }

        public async Task DeleteEnquiry(int enquiryId)
        {
            // Find the enquiry with the provided ID in the list
            var enquiryToRemove = _enquiries.FirstOrDefault(e => e.EnquiryID == enquiryId);
            if (enquiryToRemove != null)
            {
                // Remove the enquiry from the list
                _enquiries.Remove(enquiryToRemove);
            }
        }
    }
}
