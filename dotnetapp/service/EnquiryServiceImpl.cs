using dotnetapp.Models;
using dotnetapp.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class EnquiryServiceImpl : EnquiryService
    {
        private readonly IEnquiryRepository _enquiryRepository;

        public EnquiryServiceImpl(EnquiryRepo enquiryRepository)
        {
            _enquiryRepository = enquiryRepository;
        }

        public async Task<IEnumerable<Enquiry>> GetAllEnquiries()
        {
            return await _enquiryRepository.GetAllEnquiries();
        }

        public async Task<Enquiry> GetEnquiryById(int EnquiryID)
        {
            return await _enquiryRepository.GetEnquiryById(EnquiryID);
        }

        public async Task CreateEnquiry(Enquiry enquiry)
        {
            await _enquiryRepository.CreateEnquiry(enquiry);
        }

        public async Task UpdateEnquiry(Enquiry enquiry)
        {
            await _enquiryRepository.UpdateEnquiry(enquiry);
        }

        public async Task DeleteEnquiry(int EnquiryID)
        {
            await _enquiryRepository.DeleteEnquiry(EnquiryID);
        }
    }
}
