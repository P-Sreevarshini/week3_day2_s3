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

            Assert.IsTrue(connectionSuccessful, "Connection should be successful.");
        }

        [Test]
        public void Test_AddProduct_Success()
        {
            // Arrange
            int productId = 305;
            const string productName = "TestProduct";
            const decimal productRate = 10.50m;
            const int productStock = 100;

            // Act
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Add product
                        Program.AddProduct(connection);

                        // Retrieve the product ID of the newly added product
                        string query = "SELECT IDENT_CURRENT('Grocery') AS ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        productId = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception)
                {
                    Assert.Fail("Failed to add product.");
                }
            }

            // Assert
            Assert.Greater(productId, 0, "Product ID should be greater than zero after insertion.");
        }

        [Test]
        public void Test_SearchProduct_Success()
        {
            // Arrange
            const string searchTerm = "TestProduct";

            // Act
            string searchResult = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string searchQuery = "SELECT Name FROM Grocery WHERE Name LIKE @SearchTerm";
                SqlCommand command = new SqlCommand(searchQuery, connection);
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        searchResult = reader["Name"].ToString();
                    }
                }
            }

            // Assert
            Assert.IsNotNull(searchResult, "Search result should not be null.");
            Assert.AreEqual(searchTerm, searchResult, "Search result does not match expected product name.");
        }

        [Test]
        public void Test_EditProduct_Success()
        {
            // Arrange
            const int productId = 305; 
            const string updatedProductName = "UpdatedProduct";
            const decimal updatedProductRate = 20.75m;
            const int updatedProductStock = 150;

            // Act
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        Program.EditProduct(connection);
                    }
                }
                catch (Exception)
                {
                    Assert.Fail("Failed to edit product.");
                }
            }

            // Assert
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Name, Rate, Stock FROM Grocery WHERE ID = @ProductId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string productName = reader["Name"].ToString();
                        decimal productRate = Convert.ToDecimal(reader["Rate"]);
                        int productStock = Convert.ToInt32(reader["Stock"]);

                        Assert.AreEqual(updatedProductName, productName, "Updated product name does not match.");
                        Assert.AreEqual(updatedProductRate, productRate, "Updated product rate does not match.");
                        Assert.AreEqual(updatedProductStock, productStock, "Updated product stock does not match.");
                    }
                    else
                    {
                        Assert.Fail("Product not found.");
                    }
                }
            }
        }
    }
}
