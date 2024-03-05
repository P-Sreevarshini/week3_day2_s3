using System;

namespace dotnetapp.Models
{
    public class FDAccount
    {
        public long FDAccountId { get; set; }
        public long UserId { get; set; } // Foreign key referencing User
        public long FixedDepositId { get; set; }

        public User? User { get; set; }
        public FixedDeposit? FixedDeposit { get; set; }

    }
}
