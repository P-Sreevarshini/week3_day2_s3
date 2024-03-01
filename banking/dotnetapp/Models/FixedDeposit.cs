// FixedDeposit.cs
using System;

namespace dotnetapp.Models
{
    public class FixedDeposit
    {
        public long FixedDepositId { get; set; }
        public decimal Amount { get; set; }
        public int TenureMonths { get; set; }
        public decimal InterestRate { get; set; }
        public long AccountId { get; set; }
        public Account? Account { get; set; }

    }
}
