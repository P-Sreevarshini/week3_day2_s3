using System;
using System.Data.SqlClient;

namespace GroceryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=appdbnew;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add Product");
                    Console.WriteLine("2. Display All Products");
                    Console.WriteLine("3. Delete Product");
                    Console.WriteLine("4. Search Product");
                    Console.WriteLine("5. Edit Product");

                    int option;
                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        switch (option)
                        {
                            case 1:
                                AddProduct(connection);
                                break;
                            case 4:
                                SearchProduct(connection);
                                break;
                            case 5:
                                EditProduct(connection);
                                break;
                            // Implement other options here
                            default:
                                Console.WriteLine("Invalid option selected!");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid option selected!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadKey();
        }

        static void AddProduct(SqlConnection connection)
        {
            Console.WriteLine("Enter product details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Rate: ");
            decimal rate = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Stock: ");
            int stock = Convert.ToInt32(Console.ReadLine());

            string insertQuery = "INSERT INTO Grocery (Name, Rate, Stock) VALUES (@Name, @Rate, @Stock)";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Rate", rate);
            command.Parameters.AddWithValue("@Stock", stock);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Product added successfully!");
            }
        }

        static void SearchProduct(SqlConnection connection)
        {
            Console.Write("Enter the product name to search: ");
            string searchTerm = Console.ReadLine();

            string searchQuery = "SELECT * FROM Grocery WHERE Name LIKE @SearchTerm";
            SqlCommand command = new SqlCommand(searchQuery, connection);
            command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Console.WriteLine("Search results:");
                    Console.WriteLine("ID\tName\tRate\tStock");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["ID"]}\t{reader["Name"]}\t{reader["Rate"]}\t{reader["Stock"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No matching products found.");
                }
            }
        }

        static void EditProduct(SqlConnection connection)
        {
            Console.Write("Enter the ID of the product to edit: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter new product details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Rate: ");
            decimal rate = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Stock: ");
            int stock = Convert.ToInt32(Console.ReadLine());

            string updateQuery = "UPDATE Grocery SET Name = @Name, Rate = @Rate, Stock = @Stock WHERE ID = @ID";
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Rate", rate);
            command.Parameters.AddWithValue("@Stock", stock);
            command.Parameters.AddWithValue("@ID", productId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Product updated successfully!");
            }
            else
            {
                Console.WriteLine("No product found with the specified ID.");
            }
        }
    }
}
