using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        
    //    [Authorize(Roles="Admin")]
    //     [HttpGet("admin/payment")]
    //     public async Task<IActionResult> GetAllPayments()
    //     {
    //         var payments = await _paymentService.GetAllPayments();
    //         return Ok(payments);
    //     }

    //    [Authorize(Roles="Student")]
    //     [HttpPost("student/payment")]
    //     public async Task<IActionResult> CreatePayment(Payment payment)
    //     {
    //         await _paymentService.CreatePayment(payment);
    //         // return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentID }, payment);
    //         return Ok(payment); // Return the created payment directly

    //     }
    }
}