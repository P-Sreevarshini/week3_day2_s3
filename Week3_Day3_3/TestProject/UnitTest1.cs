using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Transactions;

namespace GroceryManagement.Tests
{
    public class ProgramTests
    {
        private const string ConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=GroceryDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

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
        [Test]
public void Test_AddProduct()
{
    using (SqlConnection connection = new SqlConnection(ConnectionString))
    {
        connection.Open();
        // Add a product
        AddProduct(connection);

        // Verify product has been added
        Assert.IsTrue(ProductExists(connection, 1001));

        // Cleanup: Delete the added product
        DeleteProduct(connection, 1001);
    }
}

private void AddProduct(SqlConnection connection)
{
    string insertQuery = "INSERT INTO Grocery (ID, Name, Rate, Stock) " +
                         "VALUES (1001, 'TestProduct', 10.50, 100)";
    SqlCommand command = new SqlCommand(insertQuery, connection);
    command.ExecuteNonQuery();
}

private void DeleteProduct(SqlConnection connection, int productId)
{
    string deleteQuery = "DELETE FROM Grocery WHERE ID = @ProductId";
    SqlCommand command = new SqlCommand(deleteQuery, connection);
    command.Parameters.AddWithValue("@ProductId", productId);
    command.ExecuteNonQuery();
}

private bool ProductExists(SqlConnection connection, int productId)
{
    string selectQuery = "SELECT COUNT(*) FROM Grocery WHERE ID = @ProductId";
    SqlCommand command = new SqlCommand(selectQuery, connection);
    command.Parameters.AddWithValue("@ProductId", productId);
    int count = (int)command.ExecuteScalar();
    return count > 0;
}
        
    }

}
