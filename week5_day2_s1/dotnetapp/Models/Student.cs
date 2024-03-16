using System.Collections.Generic;

namespace dotnetapp.Models;
public class Student
{
    public long StudentId { get; set; }
    public string StudentName { get; set; }
    public string StudentMobileNumber { get; set; }
    public List<Enquiry> Enquiries { get; set; }
    public List<Course> Courses { get; set; }
    public HashSet<Payment> Payments { get; set; }
    public User User { get; set; }
}
