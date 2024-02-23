// using System;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace dotnetapp.Models
// {
//     public class Payment
//     {
//         [Key]
//         public int PaymentID { get; set; }
       
//         public decimal AmountPaid { get; set; }
//         public DateTime PaymentDate { get; set; }
//         public string PaymentMethod { get; set; }
//         public string TransactionID { get; set; }
        
//         // Foreign key properties
//         public long UserId { get; set; }
//         public int CourseID { get; set; }
//         public long StudentId { get; set; }

//         // Navigation properties
//         [ForeignKey(nameof(UserId))]
//         public User User { get; set; }
        
//         // [ForeignKey(nameof(CourseID))]
//         public Course Course { get; set; }
        
//         // Assuming you have a Student entity
//         [ForeignKey(nameof(StudentId))]
//         public Student Student { get; set; }

//     }
// }


using System;
using System.ComponentModel.DataAnnotations;
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
        public int CourseID { get; set; }
        public long StudentId { get; set; }
        public User Users { get; set; }
        public Course Courses { get; set; }
        public Student Students { get; set; }
    }
}
