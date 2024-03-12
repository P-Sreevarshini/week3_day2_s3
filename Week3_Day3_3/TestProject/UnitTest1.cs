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

        // Assert: No assertion is needed as the method does not return a value.

        // Cleanup
        RemoveTestProduct(productId);
    }

    

   
[Test]
public void Test_SearchProduct_Success()
{
    // Arrange
    const string productName = "TestProduct";

    // Act
    string searchResult = null;
    try
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            searchResult = Program.SearchProduct(connection); // No need to pass additional arguments here
        }
    }
    catch (Exception ex)
    {
        Assert.Fail($"Failed to search product: {ex.Message}");
    }

    // Assert
    Assert.IsNotNull(searchResult, "Search result should not be null.");
    Assert.IsTrue(searchResult.Contains(productName), "Search result should contain the product name.");
}

    // [Test]
    // public void Test_EditProduct_Success()
    // {
    //     // Arrange
    //     int productId = GenerateRandomProductId();
    //     const string updatedProductName = "UpdatedProduct";
    //     const decimal updatedProductRate = 20.75m;
    //     const int updatedProductStock = 150;

    //     // Act
    //     try
    //     {
    //         using (SqlConnection connection = new SqlConnection(ConnectionString))
    //         {
    //             connection.Open();
    //             Program.EditProduct(connection); // No need to pass additional arguments here
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Assert.Fail($"Failed to edit product: {ex.Message}");
    //     }

    //     // Assert: The test verifies if the product was updated successfully.
    //     // This assertion is not directly tied to the method's return value.
    //     // It's assumed that if no exception was thrown, the product was updated successfully.
    //     Assert.Pass("Product should be updated successfully.");
    // }

//     [Test]
// public void Test_EditProduct_Success()
// {
//     // Arrange
//     int productId = GenerateRandomProductId();
//     string productName = "InitialProduct";
//     decimal productRate = 10.50m;
//     int productStock = 100;

//     // Add a product to edit
//     try
//     {
//         using (SqlConnection connection = new SqlConnection(ConnectionString))
//         {
//             connection.Open();
//             Program.AddProduct(connection, productId, productName, productRate, productStock);
//         }
//     }
//     catch (Exception ex)
//     {
//         Assert.Fail($"Failed to add product: {ex.Message}");
//     }

//     // Act (Edit the product)
//     const string updatedProductName = "UpdatedProduct";
//     const decimal updatedProductRate = 20.75m;
//     const int updatedProductStock = 150;

//     try
//     {
//         using (SqlConnection connection = new SqlConnection(ConnectionString))
//         {
//             connection.Open();
//             Program.EditProduct(connection, productId, updatedProductName, updatedProductRate, updatedProductStock);
//         }
//     }
//     catch (Exception ex)
//     {
//         Assert.Fail($"Failed to edit product: {ex.Message}");
//     }

//     // Assert: You can add assertions to check if the product was updated successfully.
//     // For example, you can perform a search for the updated product and verify its details.
// }

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
}


//         [Test]
//         public void Test_SearchProduct_Success()
//         {
//             // Arrange
//             const string searchTerm = "TestProduct";

//             // Act
//             string searchResult = null;
//             using (SqlConnection connection = new SqlConnection(ConnectionString))
//             {
//                 connection.Open();
//                 string searchQuery = "SELECT Name FROM Grocery WHERE Name LIKE @SearchTerm";
//                 SqlCommand command = new SqlCommand(searchQuery, connection);
//                 command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

//                 using (SqlDataReader reader = command.ExecuteReader())
//                 {
//                     if (reader.HasRows)
//                     {
//                         reader.Read();
//                         searchResult = reader["Name"].ToString();
//                     }
//                 }
//             }

//             // Assert
//             Assert.IsNotNull(searchResult, "Search result should not be null.");
//             Assert.AreEqual(searchTerm, searchResult, "Search result does not match expected product name.");
//         }

        // [Test]
        // public void Test_EditProduct_Success()
        // {
        //     // Arrange
        //     const int productId = 305; 
        //     const string updatedProductName = "UpdatedProduct";
        //     const decimal updatedProductRate = 20.75m;
        //     const int updatedProductStock = 150;

        //     // Act
        //     using (TransactionScope scope = new TransactionScope())
        //     {
        //         try
        //         {
        //             using (SqlConnection connection = new SqlConnection(ConnectionString))
        //             {
        //                 connection.Open();
        //                 Program.EditProduct(connection);
        //             }
        //         }
        //         catch (Exception)
        //         {
        //             Assert.Fail("Failed to edit product.");
        //         }
        //     }
        // }


            

//             // Assert
//             using (SqlConnection connection = new SqlConnection(ConnectionString))
//             {
//                 connection.Open();
//                 string query = "SELECT Name, Rate, Stock FROM Grocery WHERE ID = @ProductId";
//                 SqlCommand command = new SqlCommand(query, connection);
//                 command.Parameters.AddWithValue("@ProductId", productId);

//                 using (SqlDataReader reader = command.ExecuteReader())
//                 {
//                     if (reader.HasRows)
//                     {
//                         reader.Read();
//                         string productName = reader["Name"].ToString();
//                         decimal productRate = Convert.ToDecimal(reader["Rate"]);
//                         int productStock = Convert.ToInt32(reader["Stock"]);

//                         Assert.AreEqual(updatedProductName, productName, "Updated product name does not match.");
//                         Assert.AreEqual(updatedProductRate, productRate, "Updated product rate does not match.");
//                         Assert.AreEqual(updatedProductStock, productStock, "Updated product stock does not match.");
//                     }
//                     else
//                     {
//                         Assert.Fail("Product not found.");
//                     }
//                 }
//             }
//         }
//     }
}
