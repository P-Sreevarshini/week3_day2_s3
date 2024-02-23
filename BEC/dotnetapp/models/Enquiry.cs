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
    public int CourseID { get; set; }
    [ForeignKey(nameof(CourseID))]
    public Course? Course { get; set; }

    public long  UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
}