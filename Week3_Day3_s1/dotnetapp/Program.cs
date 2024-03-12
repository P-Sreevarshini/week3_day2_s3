// using System;
// using System.Data.SqlClient;

// namespace StudentManagement
// {
//    public class Program
//     {
//        public static void Main(string[] args)
//         {
//             string connectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=StudentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

//             try
//             {
//                 using (SqlConnection connection = new SqlConnection(connectionString))
//                 {
//                     connection.Open();
//                     Console.WriteLine("Connection successful!");
//                     Console.WriteLine("Select an option:");
//                     Console.WriteLine("1. Add Student");
//                     Console.WriteLine("2. Display All Students");

//                     int option;
//                     if (int.TryParse(Console.ReadLine(), out option))
//                     {
//                         switch (option)
//                         {
//                             case 1:
//                                 AddStudent(connection);
//                                 break;
//                             case 2:
//                                 DisplayAllStudents(connection);
//                                 break;
//                             default:
//                                 Console.WriteLine("Invalid option selected!");
//                                 break;
//                         }
//                     }
//                     else
//                     {
//                         Console.WriteLine("Invalid option selected!");
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"An error occurred: {ex.Message}");
//             }

//             Console.ReadKey();
//         }

//         public static void AddStudent(SqlConnection connection)
//         {
//             Console.WriteLine("Enter student details:");
//             Console.Write("Student ID: ");
//             int id = Convert.ToInt32(Console.ReadLine());
//             Console.Write("Student Name: ");
//             string name = Console.ReadLine();
//             Console.Write("Student Age: ");
//             int age = Convert.ToInt32(Console.ReadLine());
//             Console.Write("Student Gender: ");
//             string gender = Console.ReadLine();
//             Console.Write("Student Mobile Number: ");
//             string mobileNumber = Console.ReadLine();
//             Console.Write("Student Email: ");
//             string email = Console.ReadLine();

//             string insertQuery = "INSERT INTO Student (StudentId, StudentName, StudentAge, StudentGender, StudentMobileNumber, StudentEmail) VALUES (@ID, @Name, @Age, @Gender, @MobileNumber, @Email)";
//             SqlCommand command = new SqlCommand(insertQuery, connection);
//             command.Parameters.AddWithValue("@ID", id);
//             command.Parameters.AddWithValue("@Name", name);
//             command.Parameters.AddWithValue("@Age", age);
//             command.Parameters.AddWithValue("@Gender", gender);
//             command.Parameters.AddWithValue("@MobileNumber", mobileNumber);
//             command.Parameters.AddWithValue("@Email", email);

//             int rowsAffected = command.ExecuteNonQuery();
//             if (rowsAffected > 0)
//             {
//                 Console.WriteLine("Student added successfully!");
//             }
//         }

//         public static void DisplayAllStudents(SqlConnection connection)
//         {
//             string selectQuery = "SELECT * FROM Student";
//             SqlCommand command = new SqlCommand(selectQuery, connection);

//             using (SqlDataReader reader = command.ExecuteReader())
//             {
//                 if (reader.HasRows)
//                 {
//                     Console.WriteLine("Student Records:");
//                     Console.WriteLine("ID\tName\tAge\tGender\tMobile Number\tEmail");
//                     while (reader.Read())
//                     {
//                         Console.WriteLine($"{reader["StudentId"]}\t{reader["StudentName"]}\t{reader["StudentAge"]}\t{reader["StudentGender"]}\t{reader["StudentMobileNumber"]}\t{reader["StudentEmail"]}");
//                     }
//                 }
//                 else
//                 {
//                     Console.WriteLine("No student records found.");
//                 }
//             }
//         }
//     }
// }




using System;
using System.Data.SqlClient;

namespace StudentManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=StudentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add Student");
                    Console.WriteLine("2. Display All Students");

                    int option;
                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        switch (option)
                        {
                            case 1:
                                Console.Write("Student ID: ");
                                int studentId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Student Name: ");
                                string studentName = Console.ReadLine();
                                Console.Write("Student Age: ");
                                int studentAge = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Student Gender: ");
                                string studentGender = Console.ReadLine();
                                Console.Write("Student Mobile Number: ");
                                string studentMobileNumber = Console.ReadLine();
                                Console.Write("Student Email: ");
                                string studentEmail = Console.ReadLine();
                                AddStudent(connection, studentId, studentName, studentAge, studentGender, studentMobileNumber, studentEmail);
                                break;
                            case 2:
                                DisplayAllStudents(connection);
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

        public static void AddStudent(SqlConnection connection, int studentId, string studentName, int studentAge, string studentGender, string studentMobileNumber, string studentEmail)
        {
            string insertQuery = "INSERT INTO Student (StudentId, StudentName, StudentAge, StudentGender, StudentMobileNumber, StudentEmail) VALUES (@ID, @Name, @Age, @Gender, @MobileNumber, @Email)";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@ID", studentId);
            command.Parameters.AddWithValue("@Name", studentName);
            command.Parameters.AddWithValue("@Age", studentAge);
            command.Parameters.AddWithValue("@Gender", studentGender);
            command.Parameters.AddWithValue("@MobileNumber", studentMobileNumber);
            command.Parameters.AddWithValue("@Email", studentEmail);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Student added successfully!");
            }
        }

        public static void DisplayAllStudents(SqlConnection connection)
        {
            string selectQuery = "SELECT * FROM Student";
            SqlCommand command = new SqlCommand(selectQuery, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Console.WriteLine("Student Records:");
                    Console.WriteLine("ID\tName\tAge\tGender\tMobile Number\tEmail");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["StudentId"]}\t{reader["StudentName"]}\t{reader["StudentAge"]}\t{reader["StudentGender"]}\t{reader["StudentMobileNumber"]}\t{reader["StudentEmail"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No student records found.");
                }
            }
        }
    }
}
