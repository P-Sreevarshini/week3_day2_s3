using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnetapp.Models
{
public class Payment
{
    [Key]
    public int PaymentID { get; set; }
   
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string TransactionID { get; set; }
    public long UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public int CourseID { get; set; }

    [ForeignKey(nameof(CourseID))]
    public Course? Course { get; set; }
}
}