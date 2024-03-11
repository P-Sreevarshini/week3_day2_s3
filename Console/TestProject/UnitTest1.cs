using NUnit.Framework;
using System;
using System.Collections.Generic;
using dotnetapp;

namespace TestProject
{
    [TestFixture]
    public class ProgramTests
    {
        private const string EmployeeConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";
        private const string DepartmentConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=DepartmentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [Test]
        public void Test_InsertEmployee_Success()
        {
            // Arrange
            const int empId = 4;
            const string empName = "Alice Cooper";
            const string email = "alice@example.com";
            const string phoneNumber = "1234567890";
            const string department = "Marketing";

            // Act
            Program.InsertEmployee(EmployeeConnectionString, empId, empName, email, phoneNumber, department);

            // Assert
            bool recordExists = RecordExists(EmployeeConnectionString, "Employee", empId);
            Assert.IsTrue(recordExists, "Inserted record should exist in the database");
        }

        [Test]
        public void Test_RetrieveEmployeeData()
        {
            // Act
            var employees = Program.RetrieveEmployees(EmployeeConnectionString);

            // Assert
            Assert.IsNotNull(employees, "Retrieved employee data should not be null");
            Assert.Greater(employees.Count, 0, "At least one employee should be retrieved");
        }

        [Test]
        public void Test_RetrieveDepartmentData()
        {
            // Act
            var departments = Program.RetrieveDepartments(DepartmentConnectionString);

            // Assert
            Assert.IsNotNull(departments, "Retrieved department data should not be null");
            Assert.Greater(departments.Count, 0, "At least one department should be retrieved");
        }

        [Test]
        public void Test_DeleteEmployee()
        {
            // Arrange
            const int empIdToDelete = 1; // Assuming employee with ID 1 exists

            // Act
            Program.DeleteEmployee(EmployeeConnectionString, empIdToDelete);

            // Assert
            bool recordExists = RecordExists(EmployeeConnectionString, "Employee", empIdToDelete);
            Assert.IsFalse(recordExists, "Deleted record should not exist in the database");
        }
    }
}