// FDRequest.cs
namespace dotnetapp.Models
{
    public class FDRequest
    {
        public long FDRequestId { get; set; }
        public long FixedDepositId { get; set; } // Foreign key referencing FixedDeposit
        public string Status { get; set; } // Pending, Approved, Rejected
        public FixedDeposit? FixedDeposit { get; set; }
    }
}
