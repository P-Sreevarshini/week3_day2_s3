using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public class UserService
    {
        private readonly List<User> _users;
        private readonly List<Student> _students;
        private readonly List<Payment> _payments;

        public UserService()
        {
            _users = new List<User>();
            _students = new List<Student>();
            _payments = new List<Payment>();
        }

        public User GetUserById(long userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

       public void CreateUser(User user)
{
    _users.Add(user);
    
    if (user.UserRole == "student")
    {
        // Create a new student instance and populate its properties
        var student = new Student
        {
            // Populate student properties from user
            StudentName = user.Username,
            StudentMobileNumber = user.MobileNumber,
            User = user // Associate user with student
        };

        // Add the student to the list of students
        _students.Add(student);
    }
}


        public void UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                // Update user properties here
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Username = user.Username;
                existingUser.MobileNumber = user.MobileNumber;
                existingUser.UserRole = user.UserRole;
            }
        }

        public void DeleteUser(long userId)
        {
            var userToRemove = _users.FirstOrDefault(u => u.UserId == userId);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
            }
        }

        public void AddPaymentToStudent(Payment payment)
        {
            _payments.Add(payment);
        }

        public Student GetStudentByUserId(long userId)
        {
            return _students.FirstOrDefault(s => s.User.UserId == userId);
        }
        public async Task<List<Payment>> GetAllPayments()
        {
            // You can return the list of payments asynchronously using Task.FromResult
            return await Task.FromResult(_payments);
        }


    }
}
