using NUnit.Framework;
using System;
using System.Data.SqlClient;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private const string ConnectionString = "User ID=sa;password=examlyMssql@123;server=localhost;Database=TestDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [SetUp]
        public void Setup()
        {
            // Create Employee and Department tables before running tests
            Program.CreateEmployeeTable(ConnectionString);
            Program.CreateDepartmentTable(ConnectionString);
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up by deleting Employee and Department tables after running tests
            Program.DropTable(ConnectionString, "Employee");
            Program.DropTable(ConnectionString, "Department");
        }

        [Test]
        public void CreateEmployeeTable_TableCreated_Success()
        {
            // Act
            bool tableExists = Program.TableExists(ConnectionString, "Employee");

            // Assert
            Assert.IsTrue(tableExists, "Employee table should exist.");
        }

        [Test]
        public void CreateDepartmentTable_TableCreated_Success()
        {
            // Act
            bool tableExists = Program.TableExists(ConnectionString, "Department");

            // Assert
            Assert.IsTrue(tableExists, "Department table should exist.");
        }

        [Test]
        public void InsertEmployee_EmployeeInserted_Success()
        {
            // Arrange
            int empId = 1;
            string empName = "John Doe";
            string email = "john@example.com";
            string phoneNumber = "1234567890";
            string department = "HR";

            // Act
            Program.InsertEmployee(ConnectionString, empId, empName, email, phoneNumber, department);

            // Assert
            bool employeeExists = Program.RecordExists(ConnectionString, "Employee", empId);
            Assert.IsTrue(employeeExists, "Employee should be inserted into the table.");
        }

        [Test]
        public void InsertDepartment_DepartmentInserted_Success()
        {
            // Arrange
            int deptId = 1;
            string deptName = "HR";
            string location = "New York";
            int employeeCount = 10;

            // Act
            Program.InsertDepartment(ConnectionString, deptId, deptName, location, employeeCount);

            // Assert
            bool departmentExists = Program.RecordExists(ConnectionString, "Department", deptId);
            Assert.IsTrue(departmentExists, "Department should be inserted into the table.");
        }

        [Test]
        public void DeleteEmployee_EmployeeDeleted_Success()
        {
            // Arrange
            int empIdToDelete = 1;
            Program.InsertEmployee(ConnectionString, empIdToDelete, "John Doe", "john@example.com", "1234567890", "HR");

            // Act
            Program.DeleteEmployee(ConnectionString, empIdToDelete);

            // Assert
            bool employeeExists = Program.RecordExists(ConnectionString, "Employee", empIdToDelete);
            Assert.IsFalse(employeeExists, "Employee should be deleted from the table.");
        }
    }
}
