using NUnit.Framework;
using System;
using dotnetapp;

namespace TestProject
{
    [TestFixture]
    public class ProgramTests
    {
        private const string EmployeeConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";
        private const string DepartmentConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=DepartmentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

        [Test]
        public void Test_RetrieveEmployeeData()
        {
            // Act
            var employees = RetrieveEmployees(EmployeeConnectionString);

            // Assert
            Assert.IsNotNull(employees, "Retrieved employee data should not be null");
            Assert.Greater(employees.Count, 0, "At least one employee should be retrieved");
        }

        [Test]
        public void Test_RetrieveDepartmentData()
        {
            // Act
            var departments = RetrieveDepartments(DepartmentConnectionString);

            // Assert
            Assert.IsNotNull(departments, "Retrieved department data should not be null");
            Assert.Greater(departments.Count, 0, "At least one department should be retrieved");
        }

        [Test]
        public void Test_DeleteEmployee()
        {
            // Arrange
            const int empIdToDelete = 1; // Assuming employee with ID 1 exists

            // Act
            DeleteEmployee(EmployeeConnectionString, empIdToDelete);

            // Assert
            bool recordExists = RecordExists(EmployeeConnectionString, "Employee", empIdToDelete);
            Assert.IsFalse(recordExists, "Deleted record should not exist in the database");
        }
        private List<Employee> RetrieveEmployees(string connectionString)
        {
            List<Employee> employees = new List<Employee>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Employee";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmpId = reader.GetInt32(0),
                        EmpName = reader.GetString(1),
                        Email = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Department = reader.GetString(4)
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }

        private List<Department> RetrieveDepartments(string connectionString)
        {
            List<Department> departments = new List<Department>();
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Department";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Department department = new Department
                    {
                        DeptId = reader.GetInt32(0),
                        DeptName = reader.GetString(1),
                        Location = reader.GetString(2),
                        EmployeeCount = reader.GetInt32(3)
                    };

                    departments.Add(department);
                }
            }

            return departments;
        }

        private void DeleteEmployee(string connectionString, int employeeId)
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
    }
}
