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
            string connectionString = _configuration.GetConnectionString("Connectionstring");

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
            string connectionString = _configuration.GetConnectionString("Connectionstring");

            // Query to count rows in Student table
            string query = "SELECT COUNT(*) FROM Student";

            // Variable to store the count
            int rowCount = 0;

            // Attempt to execute the query
            using (var connection = new SqlConnection(connectionString))
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
public void TestCourseTableHasData()
{
    // Get the connection string from appsettings.json
    string connectionString = _configuration.GetConnectionString("Connectionstring");

    // Query to count rows in Course table
    string query = "SELECT COUNT(*) FROM Course";

    // Variable to store the count
    int rowCount = 0;

    // Attempt to execute the query
    using (var connection = new SqlConnection(connectionString))
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
