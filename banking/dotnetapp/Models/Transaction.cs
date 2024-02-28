// Transaction.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }

        public long AccountId { get; set; }

        public string Type { get; set;} // Credit or Debit////``
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
