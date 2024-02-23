using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
public class Course
{
    [Key]
    public int CourseID { get; set; }
    public string CourseName { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public int Amount { get; set; }
    
    // Navigation property for related enquiries
    [JsonIgnore]
    public ICollection<Enquiry>? Enquiries { get; set; }
}
}