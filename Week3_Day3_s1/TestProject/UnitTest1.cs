using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Transactions;

namespace StudentManagement.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private const string ConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=StudentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [Test]
        public void Test_Connection_Success()
        {
            // Arrange
            bool connectionSuccessful = false;

            // Act
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
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
        public void Test_AddStudent_Success()
        {
            // Arrange
            int studentId = 1001;
            const string studentName = "John Doe";
            const int studentAge = 20;
            const string studentGender = "Male";
            const string studentMobileNumber = "1234567890";
            const string studentEmail = "john@example.com";

            // Act
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        Program.AddStudent(connection);

                        // Retrieve the student ID of the newly added student
                        string query = "SELECT IDENT_CURRENT('Student') AS ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        studentId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception)
                {
                    Assert.Fail("Failed to add student.");
                }
            }

            // Assert
            Assert.Greater(studentId, 0, "Student ID should be greater than zero after insertion.");
        }

        [Test]
        public void Test_DisplayAllStudents_Success()
        {
            // Arrange
            bool recordsFound = false;

            // Act
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Student", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    recordsFound = reader.HasRows;
                }
            }

            // Assert
            Assert.IsTrue(recordsFound, "At least one student record should be found.");
        }
    }
}
