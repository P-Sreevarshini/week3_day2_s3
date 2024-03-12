using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Data.Common;
using System.Data;


namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            // Load the appsettings.json file
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

      [Test]
        public void TestDatabaseConnection()
        {
            // Get the connection string from appsettings.json
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            // Attempt to establish a connection
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Assert.AreEqual(System.Data.ConnectionState.Open, connection.State, "Connection is not open.");
                    Console.WriteLine("Database connection test passed successfully.");
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Failed to connect to the database: {ex.Message}");
                }
            }
        }
        [Test]
        public void TestStudentTableHasData()
        {
            // Get the connection string from appsettings.json
            string connectionString = _configuration.GetConnectionString("StudentConnectionstring");
            // string studentConnectionString = "Data Source=myServerAddress;Initial Catalog=StudentDB;User Id=myUsername;Password=myPassword;";

            // Query to count rows in Student table
            string query = "SELECT COUNT(*) FROM Student";

            // Variable to store the count
            int rowCount = 0;

            // Attempt to execute the query
            using (var connection = new SqlConnection(connectionString))
                        // using (var connection = new SqlConnection(studentConnectionString))

            using (var command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    rowCount = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Failed to execute query: {ex.Message}");
                }
            }

            // Check if there are any rows in the Student table
            Assert.IsTrue(rowCount > 0, "No data found in the Student table.");
        }
    
    [Test]
    public void TestDepartmentTableHasData()
    {
        // Get the connection string from appsettings.json
        string connectionString = _configuration.GetConnectionString("DepartmentConnectionstring");
        // string departmentConnectionString = "Data Source=myServerAddress;Initial Catalog=DepartmentDB;User Id=myUsername;Password=myPassword;";


        // Query to check if Department table exists and has data
        string query = "SELECT * FROM Department";

        // Variable to store the count
        int rowCount = 0;

        // Attempt to execute the query
        using (var connection = new SqlConnection(connectionString))
                    // using (var connection = new SqlConnection(departmentConnectionString))

        using (var command = new SqlCommand(query, connection))
        {
            try
            {
                connection.Open();
                rowCount = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to execute query: {ex.Message}");
            }
        }

        // Check if there are any rows in the Department table
        Assert.IsTrue(rowCount > 0, "No data found in the Department table.");
    }
[Test]
public void TestCourseTableHasData()
{
    // Get the connection string for the database from appsettings.json
    string connectionString = _configuration.GetConnectionString("CourseConnectionString");
        // string courseConnectionString = "Data Source=myServerAddress;Initial Catalog=CourseDB;User Id=myUsername;Password=myPassword;";


    // Query to check if Course table exists and has data
    string query = "SELECT COUNT(*) FROM Course";

    // Variable to store the count
    int rowCount = 0;

    // Attempt to execute the query
    using (var connection = new SqlConnection(connectionString))
                // using (var connection = new SqlConnection(courseConnectionString))

    using (var command = new SqlCommand(query, connection))
    {
        try
        {
            connection.Open();
            rowCount = (int)command.ExecuteScalar();
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to execute query: {ex.Message}");
        }
    }

    // Check if there are any rows in the Course table
    Assert.IsTrue(rowCount > 0, "No data found in the Course table.");
}

    }
}
