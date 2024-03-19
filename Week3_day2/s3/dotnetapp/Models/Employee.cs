using System;
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{

    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [UniqueEmail(ErrorMessage = "Email must be unique")]
        public string Email { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Salary should be greater than 0")]
        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MinAge(25, ErrorMessage = "Employee must be 25 years or older")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Department is required")]

        public string Dept { get; set; }
    }

}
