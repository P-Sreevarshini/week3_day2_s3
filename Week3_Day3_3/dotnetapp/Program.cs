using System;
using System.Data.SqlClient;

namespace GroceryManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=GroceryDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add Product");
                    Console.WriteLine("2. Search Product");
                    Console.WriteLine("3. Edit Product");

                    int option;
                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                       switch (option)
{
    case 1:
        Console.Write("Enter product ID: ");
        int productId = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter product name: ");
        string productName = Console.ReadLine();
        Console.Write("Enter product rate: ");
        decimal productRate = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Enter product stock: ");
        int productStock = Convert.ToInt32(Console.ReadLine());
        AddProduct(connection, productId, productName, productRate, productStock);
        break;
    // case 2:
    //     SearchProduct(connection);
    //     break;
    case 2:
    Console.Write("Enter the product name to search: ");
    string searchTerm = Console.ReadLine(); // Read the search term from the console
    SearchProduct(connection, searchTerm); // Pass the search term to the SearchProduct method
    break;

    case 3:
        EditProduct(connection);
        break;
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

// public static void AddProduct(SqlConnection connection, int productId, string productName, decimal productRate, int productStock)
//         {
//             Console.WriteLine("Enter product details:");
//             Console.Write("ID: ");
//             int id = Convert.ToInt32(Console.ReadLine());
//             Console.Write("Name: ");
//             string name = Console.ReadLine();
//             Console.Write("Rate: ");
//             decimal rate = Convert.ToDecimal(Console.ReadLine());
//             Console.Write("Stock: ");
//             int stock = Convert.ToInt32(Console.ReadLine());

//             string insertQuery = "INSERT INTO Grocery (ID, Name, Rate, Stock) VALUES (@ID, @Name, @Rate, @Stock)";
//             SqlCommand command = new SqlCommand(insertQuery, connection);
//             command.Parameters.AddWithValue("@ID", id);
//             command.Parameters.AddWithValue("@Name", name);
//             command.Parameters.AddWithValue("@Rate", rate);
//             command.Parameters.AddWithValue("@Stock", stock);

//             int rowsAffected = command.ExecuteNonQuery();
//             if (rowsAffected > 0)
//             {
//                 Console.WriteLine("Product added successfully!");
//             }
//         }

public static void AddProduct(SqlConnection connection, int productId, string productName, decimal productRate, int productStock)
{
    string insertQuery = "INSERT INTO Grocery (ID, Name, Rate, Stock) VALUES (@ID, @Name, @Rate, @Stock)";
    SqlCommand command = new SqlCommand(insertQuery, connection);
    command.Parameters.AddWithValue("@ID", productId);
    command.Parameters.AddWithValue("@Name", productName);
    command.Parameters.AddWithValue("@Rate", productRate);
    command.Parameters.AddWithValue("@Stock", productStock);

    int rowsAffected = command.ExecuteNonQuery();
    if (rowsAffected > 0)
    {
        Console.WriteLine("Product added successfully!");
    }
}


        public static string SearchProduct(SqlConnection connection, string searchTerm)
{
    string searchQuery = "SELECT * FROM Grocery WHERE Name LIKE @SearchTerm";
    SqlCommand command = new SqlCommand(searchQuery, connection);
    command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

    string result = "";
    using (SqlDataReader reader = command.ExecuteReader())
    {
        if (reader.HasRows)
        {
            result += "Search results:\n";
            result += "ID\tName\tRate\tStock\n";
            while (reader.Read())
            {
                result += $"{reader["ID"]}\t{reader["Name"]}\t{reader["Rate"]}\t{reader["Stock"]}\n";
            }
        }
        else
        {
            result += "No matching products found.";
        }
    }
    return result;
}


        public static void EditProduct(SqlConnection connection)
        {
            Console.Write("Enter the ID of the product to edit: ");
            // int productId = Convert.ToInt32(Console.ReadLine());
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
