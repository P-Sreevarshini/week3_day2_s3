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
