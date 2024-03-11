using NUnit.Framework;
using System;
using dotnetapp; // Make sure this is the correct namespace

namespace TestProject;

public class Tests
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
            const int empId = 409;
            const string empName = "Alice Cooper";
            const string email = "alice@example.com";
            const string phoneNumber = "1234567890";
            const string department = "Marketing";

            // Act
            dotnetapp.Program.InsertEmployee(EmployeeConnectionString, empId, empName, email, phoneNumber, department);

            // Assert
            bool recordExists = dotnetapp.Program.EmpRecordExists(EmployeeConnectionString, "Employee", empId);
            Assert.IsTrue(recordExists, "Inserted record should exist in the database");

            // Delete the inserted record
            dotnetapp.Program.DeleteEmployee(EmployeeConnectionString, empId);
            bool recordDeleted = !dotnetapp.Program.EmpRecordExists(EmployeeConnectionString, "Employee", empId);
            Assert.IsTrue(recordDeleted, "Inserted record should be deleted from the database");
        }
         [Test]
        public void Test_DeleteEmployee_Success()
        {
            // Arrange
            const int empIdToDelete = 403; // Assuming employee with ID 403 exists

            // Act
            dotnetapp.Program.DeleteEmployee(EmployeeConnectionString, empIdToDelete);

            // Assert
            bool recordExists = dotnetapp.Program.EmpRecordExists(EmployeeConnectionString, "Employee", empIdToDelete);
            Assert.IsFalse(recordExists, "Deleted record should not exist in the database");
        }
     [Test]
public void Test_InsertDepartment_Success()
{
    // Arrange
    const int deptId = 400;
    const string deptName = "Marketing";
    const string location = "New York";
    const int employeeCount = 50; // Provide a value for employeeCount

    // Act
    dotnetapp.Program.InsertDepartment(DepartmentConnectionString, deptId, deptName, location, employeeCount);

    // Assert
    bool recordExists = dotnetapp.Program.DeptRecordExists(DepartmentConnectionString, "Department", deptId);
    Assert.IsTrue(recordExists, "Inserted record should exist in the database");

    // Delete the inserted record
    dotnetapp.Program.DeleteDepartment(DepartmentConnectionString, deptId);
    bool recordDeleted = !dotnetapp.Program.DeptRecordExists(DepartmentConnectionString, "Department", deptId);
    Assert.IsTrue(recordDeleted, "Inserted record should be deleted from the database");
}

[Test]
public void Test_DeleteDepartment_Success()
{
    // Arrange
    const int deptIdToDelete = 403; // Assuming department with ID 403 exists

    // Act
    dotnetapp.Program.DeleteDepartment(DepartmentConnectionString, deptIdToDelete);

    // Assert
    bool recordExists = dotnetapp.Program.DeptRecordExists(DepartmentConnectionString, "Department", deptIdToDelete);
    Assert.IsFalse(recordExists, "Deleted record should not exist in the database");
}

    }     
}