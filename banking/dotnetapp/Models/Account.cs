// Account.cs
using System;

namespace dotnetapp.Models
{
    public class Account
    {
        public long AccountId { get; set; }
        public long UserId { get; set; } // Foreign key referencing User
        public decimal Balance { get; set; }
        public string AccountType {get;set;}
        
        // Navigation property representing the user associated with this account
        public User User { get; set; }
    }
}
