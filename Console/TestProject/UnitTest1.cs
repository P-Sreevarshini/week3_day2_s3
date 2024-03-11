using NUnit.Framework;
using System;
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
            InsertEmployee(EmployeeConnectionString, empId, empName, email, phoneNumber, department);

            // Assert
            bool recordExists = RecordExists(EmployeeConnectionString, "Employee", empId);
            Assert.IsTrue(recordExists, "Inserted record should exist in the database");
        }

    }     
}
