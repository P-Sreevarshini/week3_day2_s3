using NUnit.Framework;
using System;
using System.Data.SqlClient;

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
public void AddStudent_Successfully()
{
    // Arrange
    int studentId = GenerateRandomStudentId();
    string studentName = "Jane Doe";
    int studentAge = 21;
    string studentGender = "Female";
    string studentMobileNumber = "9876543210";
    string studentEmail = "jane@example.com";

    // Act
    try
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            Program.AddStudent(connection, studentId, studentName, studentAge, studentGender, studentMobileNumber, studentEmail);
        }
    }
    catch (Exception ex)
    {
        Assert.Fail($"Failed to add student: {ex.Message}");
    }

    // Assert: No assertion is needed as the method does not return a value.

    // Cleanup
    RemoveTestStudent(studentId);
}

private int GenerateRandomStudentId()
{
    // Generate a random student ID
    Random random = new Random();
    return random.Next(10000, 99999);
}

private void RemoveTestStudent(int studentId)
{
    // Perform cleanup by removing the test student from the database
    try
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("DELETE FROM Student WHERE StudentId = @StudentId", connection))
            {
                command.Parameters.AddWithValue("@StudentId", studentId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Test student removed successfully.");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to remove test student: {ex.Message}");
    }
}


    //     [Test]
    //     public void Test_DisplayAllStudents_Success()
    //     {
    //         // Arrange
    //         bool recordsFound = false;

    //         // Act
    //         using (SqlConnection connection = new SqlConnection(ConnectionString))
    //         {
    //             connection.Open();
    //             using (SqlCommand command = new SqlCommand("SELECT * FROM Student", connection))
    //             using (SqlDataReader reader = command.ExecuteReader())
    //             {
    //                 recordsFound = reader.HasRows;
    //             }
    //         }

    //         // Assert
    //         Assert.IsTrue(recordsFound, "At least one student record should be found.");
    //     }

    //     private void RemoveTestStudent(int studentId)
    //     {
    //         // Perform cleanup by removing the test student from the database
    //         try
    //         {
    //             using (SqlConnection connection = new SqlConnection(ConnectionString))
    //             {
    //                 connection.Open();
    //                 using (SqlCommand command = new SqlCommand("DELETE FROM Student WHERE StudentId = @StudentId", connection))
    //                 {
    //                     command.Parameters.AddWithValue("@StudentId", studentId);
    //                     int rowsAffected = command.ExecuteNonQuery();
    //                     if (rowsAffected > 0)
    //                     {
    //                         Console.WriteLine("Test student removed successfully.");
    //                     }
    //                 }
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine($"Failed to remove test student: {ex.Message}");
    //         }
        // }
    }
}
