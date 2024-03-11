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

        [Test]
        public void Test_InsertEmployee_Success()
        {
            // Arrange
            const int empId = 123;
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

        // [Test]
        // public void Test_InsertEmployee_DuplicateId()
        // {
        //     // Arrange
        //     const int empId = 1; // Assuming an employee with ID 1 already exists
        //     const string empName = "Bob Smith";
        //     const string email = "bob@example.com";
        //     const string phoneNumber = "9876543210";
        //     const string department = "HR";

        //     // Act & Assert
        //     Assert.Throws<SqlException>(() => Program.InsertEmployee(EmployeeConnectionString, empId, empName, email, phoneNumber, department));
        // }

        [Test]
        public void Test_DeleteEmployee_Success()
        {
            // Arrange
            const int empIdToDelete = 1; // Assuming employee with ID 1 exists

            // Act
            Program.DeleteEmployee(EmployeeConnectionString, empIdToDelete);

            // Assert
            bool recordExists = RecordExists(EmployeeConnectionString, "Employee", empIdToDelete);
            Assert.IsFalse(recordExists, "Deleted record should not exist in the database");
        }

        [Test]
        public void Test_DeleteEmployee_NonExistentId()
        {
            // Arrange
            const int empIdToDelete = 100; // Assuming employee with ID 100 does not exist

            // Act & Assert
            Assert.DoesNotThrow(() => Program.DeleteEmployee(EmployeeConnectionString, empIdToDelete));
        }

        private bool RecordExists(string connectionString, string tableName, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
