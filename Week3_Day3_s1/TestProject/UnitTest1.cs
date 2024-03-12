using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Transactions;
using Moq;

namespace StudentManagement.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private const string ConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=StudentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [Test]
        public void Test_Connection_Success()
        {
            // Assert.Pass();
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

        // [Test]
        // public void Test_AddStudent_Success()
        // {
        //     // Arrange
        //     int studentId = 1001;
        //     const string studentName = "John Doe";
        //     const int studentAge = 20;
        //     const string studentGender = "Male";
        //     const string studentMobileNumber = "1234567890";
        //     const string studentEmail = "john@example.com";

        //     // Act
        //     using (TransactionScope scope = new TransactionScope())
        //     {
        //         try
        //         {
        //             using (SqlConnection connection = new SqlConnection(ConnectionString))
        //             {
        //                 connection.Open();
        //                 Program.AddStudent(connection);

        //                 // Retrieve the student ID of the newly added student
        //                 string query = "SELECT IDENT_CURRENT('Student') AS ID";
        //                 SqlCommand command = new SqlCommand(query, connection);
        //                 studentId = Convert.ToInt32(command.ExecuteScalar());
        //             }
        //         }
        //         catch (Exception)
        //         {
        //             Assert.Fail("Failed to add student.");
        //         }
        //     }

        //     // Assert
        //     Assert.Greater(studentId, 0, "Student ID should be greater than zero after insertion.");
        // }
        
         [Test]
        public void AddStudent_Successfully()
        {
            // Arrange
            int expectedRowsAffected = 1; // Assuming one row will be affected when adding a student
            int studentId = 1001;
            string studentName = "John Doe";
            int studentAge = 20;
            string studentGender = "Male";
            string studentMobileNumber = "1234567890";
            string studentEmail = "john@example.com";

            var mockConnection = new Mock<SqlConnection>();
            mockConnection.Setup(c => c.Open());

            var commandMock = new Mock<SqlCommand>();
            commandMock.Setup(c => c.ExecuteNonQuery()).Returns(expectedRowsAffected);

            mockConnection.Setup(c => c.CreateCommand()).Returns(commandMock.Object);

            // Act
            try
            {
                Program.AddStudent(mockConnection.Object);
            }
            catch (SqlException)
            {
                Assert.Fail("Failed to add student.");
            }

            // Assert
            commandMock.Verify(c => c.Parameters.AddWithValue("@ID", studentId), Times.Once);
            commandMock.Verify(c => c.Parameters.AddWithValue("@Name", studentName), Times.Once);
            commandMock.Verify(c => c.Parameters.AddWithValue("@Age", studentAge), Times.Once);
            commandMock.Verify(c => c.Parameters.AddWithValue("@Gender", studentGender), Times.Once);
            commandMock.Verify(c => c.Parameters.AddWithValue("@MobileNumber", studentMobileNumber), Times.Once);
            commandMock.Verify(c => c.Parameters.AddWithValue("@Email", studentEmail), Times.Once);
            commandMock.Verify(c => c.ExecuteNonQuery(), Times.Once);
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
