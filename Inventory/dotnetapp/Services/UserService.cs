using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
public async Task CreateStudent(Student student)
{
    // Check if the user already exists
    var existingUser = await _context.Users.FindAsync(student.UserId);
    if (existingUser == null)
    {
        // If the user doesn't exist, create a new user
        var newUser = new User
        {
            // Populate user properties here
            // For example:
            Username = student.StudentName,
            MobileNumber = student.StudentMobileNumber,
            // Set other user properties as needed
            UserRole = "student"
        };

        // Add the new user to the Users table
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync(); // Save changes to generate UserId

        // Assign the generated UserId to the student
        student.UserId = newUser.UserId;
    }

    // Add the student to the Students table
    _context.Students.Add(student);
    await _context.SaveChangesAsync();
}



        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(long userId)
        {
            var userToRemove = await _context.Users.FindAsync(userId);
            if (userToRemove != null)
            {
                _context.Users.Remove(userToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Student> GetStudentByUserId(long userId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.User.UserId == userId);
        }

        public async Task AddPaymentToStudent(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}
