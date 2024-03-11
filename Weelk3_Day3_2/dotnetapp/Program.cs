using System;
using System.Data.SqlClient;

namespace AdoNetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connection string for appdb
            string appdbConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=appdb;trusted_connection=false;Persist Security Info=False;Encrypt=False";

            // Connection string for EmployeeDB
            string employeeConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False";

            // Connection string for DepartmentDB
            string departmentConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=DepartmentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False";

            // Create Employee table
            CreateEmployeeTable(employeeConnectionString);

            // Create Department table
            CreateDepartmentTable(departmentConnectionString);

            // Display all Employees
            DisplayEmployees(employeeConnectionString);

            // Display all Departments
            DisplayDepartments(departmentConnectionString);

            // Delete an Employee
            DeleteEmployee(employeeConnectionString, employeeIdToDelete);

            // Delete a Department
            DeleteDepartment(departmentConnectionString, departmentIdToDelete);
        }

        static void CreateEmployeeTable(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE Employee (EmpId INT PRIMARY KEY, EmpName VARCHAR(50), Email VARCHAR(50), PhoneNumber VARCHAR(20), Department VARCHAR(50))";

                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("Employee table created successfully.");
            }
        }

        static void CreateDepartmentTable(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE Department (DeptId INT PRIMARY KEY, DeptName VARCHAR(50), Location VARCHAR(50), EmployeeCount INT)";

                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("Department table created successfully.");
            }
        }

        static void DisplayEmployees(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        static void DeleteEmployee(string connectionString, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = $"DELETE FROM Employee WHERE EmpId = {employeeId}";

                SqlCommand command = new SqlCommand(deleteQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Deleted {rowsAffected} employee(s).");
            }
        }

        static void DeleteDepartment(string connectionString, int departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = $"DELETE FROM Department WHERE DeptId = {departmentId}";

                SqlCommand command = new SqlCommand(deleteQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Deleted {rowsAffected} department(s).");
            }
        }
    }
}
