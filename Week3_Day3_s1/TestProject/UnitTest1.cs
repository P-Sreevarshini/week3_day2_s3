using NUnit.Framework;
using System;
using System.Data.SqlClient;

namespace StudentManagement.Tests
{
    [TestFixture]
    public class Tests
    {
        private string connectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=StudentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [Test]
        public void ConnectionTest()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Assert.Pass("Connection successful!");
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Connection failed: {ex.Message}");
                }
            }
        }

        [Test]
        public void AddStudentTest()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Program.AddStudent(connection, 1001, "John Doe", 20, "Male", "1234567890", "john@example.com");
                    Assert.Pass("Student added successfully!");
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Failed to add student: {ex.Message}");
                }
                finally
                {
                    Program.DeleteStudent(connection, 1001); // Clean up
                }
            }
        }

        [Test]
        public void DisplayStudentRecordsTest()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Program.DisplayAllStudents(connection);
                    Assert.Pass("Student records displayed successfully!");
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Failed to display student records: {ex.Message}");
                }
            }
        }
    }
}
