using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class DatabaseTests
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
        public void TestConnectionString()
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
        public void TestEmployeeTableHasData()
        {
            // Get the connection string for the EmployeeDB from appsettings.json
            string employeeConnectionString = _configuration.GetConnectionString("EmployeeConnectionString");

            // Query to count rows in Employee table
            string query = "SELECT COUNT(*) FROM Employee";

            // Variable to store the count
            int rowCount = 0;

            // Attempt to execute the query
            using (var connection = new SqlConnection(employeeConnectionString))
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

            // Check if there are any rows in the Employee table
            Assert.IsTrue(rowCount > 0, "No data found in the Employee table.");
        }

        [Test]
        public void TestDepartmentTableHasData()
        {
            // Get the connection string for the DepartmentDB from appsettings.json
            string departmentConnectionString = _configuration.GetConnectionString("DepartmentConnectionString");

            // Query to count rows in Department table
            string query = "SELECT COUNT(*) FROM Department";

            // Variable to store the count
            int rowCount = 0;

            // Attempt to execute the query
            using (var connection = new SqlConnection(departmentConnectionString))
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
            Assert.IsTrue(rowCount > 0, "No data found in the Department table.");
        }
        
    }
}
