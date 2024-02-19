public class Enquiry
{
    public int EnquiryID { get; set; }
    public DateTime EnquiryDate { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string EmailID { get; set; }
    public string EnquiryType { get; set; }
    
    // Foreign key for the related course
    public int CourseID { get; set; }
    // Navigation property for the related course
    public Course Course { get; set; }
}