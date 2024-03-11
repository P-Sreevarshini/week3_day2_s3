using NUnit.Framework;
using System;

namespace TestProject
{
    [TestFixture]
    public class ProgramTests
    {
        private const string EmployeeConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";
        private const string DepartmentConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=DepartmentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [Test]
        public void Test_CreateEmployeeTable()
        {
            // Arrange
            bool tableExistsBefore = TableExists(EmployeeConnectionString, "Employee");

            // Act
            CreateEmployeeTable(EmployeeConnectionString);

            // Assert
            bool tableExistsAfter = TableExists(EmployeeConnectionString, "Employee");
            Assert.IsFalse(tableExistsBefore, "Table should not exist before creation");
            Assert.IsTrue(tableExistsAfter, "Table should exist after creation");
        }

        [Test]
        public void Test_CreateDepartmentTable()
        {
            // Arrange
            bool tableExistsBefore = TableExists(DepartmentConnectionString, "Department");

            // Act
            CreateDepartmentTable(DepartmentConnectionString);

            // Assert
            bool tableExistsAfter = TableExists(DepartmentConnectionString, "Department");
            Assert.IsFalse(tableExistsBefore, "Table should not exist before creation");
            Assert.IsTrue(tableExistsAfter, "Table should exist after creation");
        }

        [Test]
        public void Test_InsertEmployee()
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

        [Test]
        public void Test_InsertDepartment()
        {
            // Arrange
            const int deptId = 4;
            const string deptName = "Marketing";
            const string location = "New York";
            const int employeeCount = 20;

            // Act
            InsertDepartment(DepartmentConnectionString, deptId, deptName, location, employeeCount);

            // Assert
            bool recordExists = RecordExists(DepartmentConnectionString, "Department", deptId);
            Assert.IsTrue(recordExists, "Inserted record should exist in the database");
        }

        [Test]
        public void Test_DeleteEmployee()
        {
            // Arrange
            const int employeeIdToDelete = 1; // Assuming employee with ID 1 exists

            // Act
            DeleteEmployee(EmployeeConnectionString, employeeIdToDelete);

            // Assert
            bool recordExists = RecordExists(EmployeeConnectionString, "Employee", employeeIdToDelete);
            Assert.IsFalse(recordExists, "Deleted record should not exist in the database");
        }
    }
}
