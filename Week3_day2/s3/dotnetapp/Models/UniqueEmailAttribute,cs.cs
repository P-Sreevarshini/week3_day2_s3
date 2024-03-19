using System;
using System.ComponentModel.DataAnnotations;
using dotnetapp.Data;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var employee = validationContext.ObjectInstance as Employee;

            if (dbContext.Employees.Any(e => e.Email == value.ToString() && e.Id != employee.Id))
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    public class MinAgeAttribute : ValidationAttribute
    {
        private int _minAge;

        public MinAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateOfBirth = (DateTime)value;
            int age = DateTime.Now.Year - dateOfBirth.Year;

            if (dateOfBirth > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            if (age < _minAge)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}