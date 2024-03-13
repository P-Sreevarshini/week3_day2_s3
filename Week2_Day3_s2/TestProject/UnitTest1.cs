using System;
using System.Data.SqlClient;
using NUnit.Framework;

namespace EmployeeManagement.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private string connectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";
         [Test]
        public void Test_SQL_Connection()
        {
            // Arrange
            bool connectionSuccessful = false;

            // Act
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connectionSuccessful = true;
                }
            }
            catch (Exception)
            {
                connectionSuccessful = false;
            }

            // Assert
            Assert.IsTrue(connectionSuccessful, "Connection should be successful.");
        }
        [Test]
        public void Test_Add_Employee_Method_Exist()
        {
            var programType = typeof(Program);
            var addEmployeeMethod = programType.GetMethod("AddEmployee");
            Assert.IsNotNull(addEmployeeMethod);
        }

        [Test]
        public void Test_Delete_Employee_Method_Exist()
        {
            var programType = typeof(Program);
            var deleteEmployeeMethod = programType.GetMethod("DeleteEmployee");
            Assert.IsNotNull(deleteEmployeeMethod);
        }
        [Test]
        public void Test_Add_Employee()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Add an employee
                AddEmployee(connection);

                // Verify employee has been added
                Assert.IsTrue(EmployeeExists(connection, 1001));

                // Cleanup: Delete the added employee
                DeleteEmployee(connection, 1001);
            }
        }

        [Test]
        public void Test_Delete_Employee()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Add an employee for deletion
                AddEmployee(connection);

                // Delete the added employee
                DeleteEmployee(connection, 1001);

                // Verify employee has been deleted
                Assert.IsFalse(EmployeeExists(connection, 1001));
            }
        }

        private void AddEmployee(SqlConnection connection)
        {
            string insertQuery = "INSERT INTO Employee (EmpId, EmpName, Email, PhoneNumber, Department) " +
                                 "VALUES (1001, 'John Doe', 'john@example.com', '1234567890', 'IT')";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();
        }

        private void DeleteEmployee(SqlConnection connection, int empId)
        {
            string deleteQuery = "DELETE FROM Employee WHERE EmpId = @EmpId";
            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            command.ExecuteNonQuery();
        }

        private bool EmployeeExists(SqlConnection connection, int empId)
        {
            string selectQuery = "SELECT COUNT(*) FROM Employee WHERE EmpId = @EmpId";
            SqlCommand command = new SqlCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }
}
