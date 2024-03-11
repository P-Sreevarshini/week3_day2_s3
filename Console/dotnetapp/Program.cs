using System;
using System.Data.SqlClient;

namespace AdoNetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connection string for both databases
            string connectionString = "User ID=sa;password=examlyMssql@123;server=localhost;Database=demo;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

            // Create EmployeeDB database and tables
            CreateEmployeeDB(connectionString);

            // Create DepartmentDB database and tables
            CreateDepartmentDB(connectionString);

            // Display all Employees
            DisplayEmployees(connectionString);

            // Display all Departments
            DisplayDepartments(connectionString);
        }

        static void CreateEmployeeDB(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create Employee table
                string createTableQuery = "CREATE DATABASE EmployeeDB; " +
                                          "USE EmployeeDB; " +
                                          "CREATE TABLE Employee (EmpId INT PRIMARY KEY, EmpName VARCHAR(50), Email VARCHAR(50), PhoneNumber VARCHAR(20), Department VARCHAR(50));";
                
                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("EmployeeDB and Employee table created successfully.");
            }
        }

        static void CreateDepartmentDB(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create Department table
                string createTableQuery = "CREATE DATABASE DepartmentDB; " +
                                          "USE DepartmentDB; " +
                                          "CREATE TABLE Department (DeptId INT PRIMARY KEY, DeptName VARCHAR(50), Location VARCHAR(50), EmployeeCount INT);";

                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("DepartmentDB and Department table created successfully.");
            }
        }

        static void DisplayEmployees(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString + "Database=EmployeeDB;"))
            {
                connection.Open();

                string query = "SELECT * FROM Employee";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Employees:");
                while (reader.Read())
                {
                    Console.WriteLine($"EmpId: {reader.GetInt32(0)}, EmpName: {reader.GetString(1)}, Email: {reader.GetString(2)}, PhoneNumber: {reader.GetString(3)}, Department: {reader.GetString(4)}");
                }
            }
        }

        static void DisplayDepartments(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString + "Database=DepartmentDB;"))
            {
                connection.Open();

                string query = "SELECT * FROM Department";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Departments:");
                while (reader.Read())
                {
                    Console.WriteLine($"DeptId: {reader.GetInt32(0)}, DeptName: {reader.GetString(1)}, Location: {reader.GetString(2)}, EmployeeCount: {reader.GetInt32(3)}");
                }
            }
        }
    }
}
