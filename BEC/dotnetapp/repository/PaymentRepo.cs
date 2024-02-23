using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Repository
{
    public class PaymentRepo
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _context.Payments
                        .Include(b => b.User) 
                         .Include(c => c.Course) .ToListAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task CreatePayment(Payment payment)
        { 
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            
            // Load related entities
            await _context.Entry(payment)
                .Reference(e => e.Course)
                .LoadAsync();

            await _context.Entry(payment)
                .Reference(e => e.User)
                .LoadAsync();
        }
    


        public async Task UpdatePayment(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}