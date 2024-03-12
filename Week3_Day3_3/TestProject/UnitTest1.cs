using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Transactions;

namespace GroceryManagement.Tests
{
    public class ProgramTests
    {
        private const string ConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=appdbnew;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [Test]
        public void Test_SQL_Connection()
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

            Assert.IsTrue(connectionSuccessful, "Connection should be successful.");
        }
        

    [Test]
    public void Test_Add_Product()
    {
        // Arrange
        int productId = GenerateRandomProductId();
        string productName = "TestProduct";
        decimal productRate = 10.50m;
        int productStock = 100;

        // Act
        try
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Program.AddProduct(connection, productId, productName, productRate, productStock);
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to add product: {ex.Message}");
        }

        RemoveTestProduct(productId);
    }

        private int GenerateRandomProductId()
        {
            // Generate a random product ID
            Random random = new Random();
            return random.Next(10000, 99999);
        }

        private void RemoveTestProduct(int productId)
        {
            // Perform cleanup by removing the test product from the database
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Grocery WHERE ID = @ProductId", connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Test product removed successfully.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to remove test product: {ex.Message}");
            }
        }
        [Test]
        public void Test_AddProduct_Method_Exists()
        {
            var methodInfo = typeof(Program).GetMethod("AddProduct");
            Assert.IsNotNull(methodInfo);
        }

        [Test]
        public void Test_SearchProduct_Method_Exists()
        {
            var methodInfo = typeof(Program).GetMethod("SearchProduct");
            Assert.IsNotNull(methodInfo);
        }

        [Test]
        public void Test_EditProduct_Method_Exists()
        {
            var methodInfo = typeof(Program).GetMethod("EditProduct");
            Assert.IsNotNull(methodInfo);
        }
    }

}
