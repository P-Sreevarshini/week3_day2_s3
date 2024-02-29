// FixedDeposit.cs
using System;

namespace dotnetapp.Models
{
    public class FixedDeposit
    {
        public long FixedDepositId { get; set; }
        public long UserId { get; set; } // Foreign key referencing User
        public decimal Amount { get; set; }
        public int TenureMonths { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; }

        public User? User { get; set; }
    }
}
