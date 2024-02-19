using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnetapp.Models
{
public class Enquiry
{
    [Key]
    public int EnquiryID { get; set; }
    public DateTime EnquiryDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string EmailID { get; set; }
    public string EnquiryType { get; set; }
    
    // Foreign key for the related course
    public int CourseID { get; set; }
    // Navigation property for the related course
    [ForeignKey(nameof(CourseID))]
    public Course? Course { get; set; }
    public long UserId { get; set; }
    // Navigation property for the related course
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
}