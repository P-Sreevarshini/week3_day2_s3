using System;
using System.Data.SqlClient;

namespace GroceryManagement
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
                    Console.WriteLine("2. Add Department");
                    Console.WriteLine("3. Delete Employee");
                    Console.WriteLine("4. Delete Department");

                    int option;
                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        switch (option)
                        {
                            case 1:
                                AddEmployee(connection);
                                break;
                            case 2:
                                AddDepartment(connection);
                                break;
                            case 3:
                                DeleteEmployee(connection);
                                break;
                            case 4:
                                DeleteDepartment(connection);
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

        public static void AddDepartment(SqlConnection connection)
        {
            Console.WriteLine("Enter department details:");
            Console.Write("Department ID: ");
            int deptId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Department Name: ");
            string deptName = Console.ReadLine();
            Console.Write("Location: ");
            string location = Console.ReadLine();
            Console.Write("Employee Count: ");
            int employeeCount = Convert.ToInt32(Console.ReadLine());

            string insertQuery = "INSERT INTO Department (DeptId, DeptName, Location, EmployeeCount) " +
                                 "VALUES (@DeptId, @DeptName, @Location, @EmployeeCount)";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@DeptId", deptId);
            command.Parameters.AddWithValue("@DeptName", deptName);
            command.Parameters.AddWithValue("@Location", location);
            command.Parameters.AddWithValue("@EmployeeCount", employeeCount);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Department added successfully!");
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

        public static void DeleteDepartment(SqlConnection connection)
        {
            Console.Write("Enter the Department ID to delete: ");
            int deptId = Convert.ToInt32(Console.ReadLine());

            string deleteQuery = "DELETE FROM Department WHERE DeptId = @DeptId";
            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@DeptId", deptId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Department deleted successfully!");
            }
            else
            {
                Console.WriteLine("No department found with the specified ID.");
            }
        }
    }
}
