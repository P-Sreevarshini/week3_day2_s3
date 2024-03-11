using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Data.Common;

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
            string connectionString = _configuration.GetConnectionString("conn");

            // Attempt to establish a connection
            using (var connection = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection())
            {
                if (connection != null)
                {
                    connection.ConnectionString = connectionString;
                    try
                    {
                        connection.Open();
                        Assert.AreEqual(ConnectionState.Open, connection.State, "Connection is not open.");
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail($"Failed to connect to the database: {ex.Message}");
                    }
                }
                else
                {
                    Assert.Fail("Failed to create database connection.");
                }
            }
        }
    }
}
