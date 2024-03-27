using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using dotnetapp.Models;
using System.ComponentModel.DataAnnotations;
using static NuGet.Packaging.PackagingConstants;
using System.Numerics;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void Employee_Properties_Have_RequiredAttribute()
        {
            var count = 0;

            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Name")
                {
                    var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                    Assert.NotNull(requiredAttribute, $"{property.Name} should have a RequiredAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Employee_Properties_Have_EmailAddressAttribute()
        {
            var count = 0;
            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Email")
                {
                    var emailAttribute = property.GetCustomAttribute<EmailAddressAttribute>();
                    Assert.NotNull(emailAttribute, $"{property.Name} should have an EmailAddressAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
            
        }

        [Test]
        public void Employee_Properties_Have_RangeAttribute()
        {
            var count = 0;

            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Salary")
                {
                    var rangeAttribute = property.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                    Assert.NotNull(rangeAttribute, $"{property.Name} should have a RangeAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Employee_Properties_Have_DataTypeAttribute()
        {
            var count = 0;
            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Dob")
                {
                    var dataTypeAttribute = property.GetCustomAttribute<DataTypeAttribute>();
                    Assert.NotNull(dataTypeAttribute, $"{property.Name} should have a DataTypeAttribute.");
                    count++;
                    break;
                } 
            }
            if(count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Employee_Properties_Have_MinAgeAttribute()
        {
            var count = 0;
            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Dob")
                {
                    var minAgeAttribute = property.GetCustomAttribute<MinAgeAttribute>();
                    Assert.NotNull(minAgeAttribute, $"{property.Name} should have a MinAgeAttribute.");
                    count++;
                    break;
                }
            }
            if( count == 0)
            { Assert.Fail(); }
        }

        //[TestCase("Alice Brown", "alice@example.com", 1500, "1990-01-01", "HR", null)] // Valid case, no error expected
        [Test]
        public void Employee_Property_Name_Validation()
        {
            var employee1 = new Dictionary<string, object>
            {
                { "Name", "" },
                { "Email", "a@gmail.com" },
                { "Salary", 1500 },
                { "Dob", DateTime.Parse("1990-01-01") },
                { "Dept", "HR" }
            };
            var employee = CreatePlayerFromDictionary(employee1);
            string expectedErrorMessage = "Name is required";
            var context = new ValidationContext(employee, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(employee, context, results);

            if (expectedErrorMessage == null)
            {
                Assert.IsTrue(isValid);
            }
            else
            {
                Assert.IsFalse(isValid);
                var errorMessages = results.Select(result => result.ErrorMessage).ToList();
                Assert.Contains(expectedErrorMessage, errorMessages);
            }
        }

        public Employee CreatePlayerFromDictionary(Dictionary<string, object> data)
        {
            var player = new Employee();
            foreach (var kvp in data)
            {
                var propertyInfo = typeof(Employee).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType == typeof(decimal) && kvp.Value is int intValue)
                    {
                        propertyInfo.SetValue(player, (decimal)intValue);
                    }
                    else
                    {
                        propertyInfo.SetValue(player, kvp.Value);
                    }
                }
            }
            return player;
        }


        [Test]
        public void Employee_Property_Email_Validation()
        {
           
            var employee1 = new Dictionary<string, object>
            {
                { "Name", "asd" },
                { "Email", "" },
                { "Salary", 1500 },
                { "Dob", DateTime.Parse("1990-01-01") },
                { "Dept", "HR" }
            };
            var employee = CreatePlayerFromDictionary(employee1);
            string expectedErrorMessage = "Email is required";
            var context = new ValidationContext(employee, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(employee, context, results);

            if (expectedErrorMessage == null)
            {
                Assert.IsTrue(isValid);
            }
            else
            {
                Assert.IsFalse(isValid);
                var errorMessages = results.Select(result => result.ErrorMessage).ToList();
                Assert.Contains(expectedErrorMessage, errorMessages);
            }
        }
        [Test]
        public void Employee_Property_MinAge_Validation()
        {
            var employeeData = new Dictionary<string, object>
            {
                { "Name", "John Doe" },
                { "Email", "john@example.com" },
                { "Salary", 1500 },
                { "Dob", DateTime.Now.AddYears(-24).AddDays(1) }, // Adjusted to ensure below minimum age
                { "Dept", "HR" }
            };
            var employee = CreatePlayerFromDictionary(employeeData);
            string expectedErrorMessage = "Employee must be 25 years or older";
            var context = new ValidationContext(employee, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(employee, context, results);

            if (expectedErrorMessage == null)
            {
                Assert.IsTrue(isValid);
            }
            
        }
        
       
        private Employee CreateEmployeeFromDictionary(Dictionary<string, object> data)
        {
            var employee = new Employee();
            foreach (var kvp in data)
            {
                var propertyInfo = typeof(Employee).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType == typeof(decimal) && kvp.Value is int intValue)
                    {
                        propertyInfo.SetValue(employee, (decimal)intValue);
                    }
                    else
                    {
                        propertyInfo.SetValue(employee, kvp.Value);
                    }
                }
            }
            return employee;
        }
        [Test]
        public void Employee_Properties_Have_UniqueEmailAttribute()
        {
            Type employeeType = typeof(Employee);
            PropertyInfo emailProperty = employeeType.GetProperty("Email");

            var uniqueEmailAttribute = emailProperty.GetCustomAttribute<UniqueEmailAttribute>();

            Assert.IsNotNull(uniqueEmailAttribute, "UniqueEmail attribute should be applied to the Email property");
        }

    }
}
