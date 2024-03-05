using System;

namespace dotnetapp.Models
{
    public class FDAccount
    {
        public long FDAccountId { get; set; }
        public long UserId { get; set; } 
        public string Status { get; set; } // Pending, Approved, Rejected

        public long FixedDepositId { get; set; }

        public User? User { get; set; }
        public FixedDeposit? FixedDeposit { get; set; }

    }
}
