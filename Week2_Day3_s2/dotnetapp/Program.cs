using System;
using System.Data.SqlClient;

namespace EmployeeManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add Employee");
                    Console.WriteLine("2. Delete Employee");

                    int option;
                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        switch (option)
                        {
                            case 1:
                                AddEmployee(connection);
                                break;
                            case 2:
                                 DeleteEmployee(connection);
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

        public static void AddEmployee(SqlConnection connection)
        {
            Console.WriteLine("Enter employee details:");
            Console.Write("Employee ID: ");
            int empId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Employee Name: ");
            string empName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Department: ");
            string department = Console.ReadLine();

            string insertQuery = "INSERT INTO Employee (EmpId, EmpName, Email, PhoneNumber, Department) " +
                                 "VALUES (@EmpId, @EmpName, @Email, @PhoneNumber, @Department)";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@EmpId", empId);
            command.Parameters.AddWithValue("@EmpName", empName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            command.Parameters.AddWithValue("@Department", department);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Employee added successfully!");
            }
        }

       

        public static void DeleteEmployee(SqlConnection connection)
        {
            Console.Write("Enter the Employee ID to delete: ");
            int empId = Convert.ToInt32(Console.ReadLine());

            string deleteQuery = "DELETE FROM Employee WHERE EmpId = @EmpId";
            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@EmpId", empId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Employee deleted successfully!");
            }
            else
            {
                Console.WriteLine("No employee found with the specified ID.");
            }
        }
    }
}
