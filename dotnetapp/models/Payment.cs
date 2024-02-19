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
    public long UserId { get; set; }
    public int CourseID { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string TransactionID { get; set; }
    
    // Navigation property for the related user
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    // Navigation property for the related course
    [ForeignKey(nameof(CourseID))]
    public Course? Course { get; set; }
}
}