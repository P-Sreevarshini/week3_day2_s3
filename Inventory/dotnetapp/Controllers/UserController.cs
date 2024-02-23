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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
[Authorize(Roles="Admin,Student")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(long userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        
        [Authorize(Roles="Admin,Student")]

        [HttpPost("student")]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }
        [Authorize(Roles="Student")]

        [HttpPut("student/{id}")]
        public async Task<IActionResult> UpdateUser(long studentId, User user)
        {
            if (studentId != user.UserId)
            {
                return BadRequest();
            }

            var existingUser = await _userService.GetUserById(studentId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.UpdateUser(user);

            return NoContent();
        }
        [Authorize(Roles="Admin,Student")]

        [HttpDelete("student/{id}")]
        public async Task<IActionResult> DeleteUser(long studentId)
        {
            var existingUser = await _userService.GetUserById(studentId);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(studentId);

            return NoContent();
        }
        [Authorize(Roles="Student")]

        [HttpPost("student/payment")]
        public async Task<IActionResult> PostStudentPayment(long studentId, Payment payment)
        {
            payment.StudentId = studentId;
            await _userService.AddPaymentToStudent(payment);
            return CreatedAtAction(nameof(PostStudentPayment), new { studentId }, payment);
        }
        [Authorize(Roles="Admin,Student")]

        [HttpGet("student/user/{userId}")]
        public async Task<IActionResult> GetStudentByUserId(long userId)
        {
            var student = await _userService.GetStudentByUserId(userId);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [Authorize(Roles="Admin")]

        [HttpGet("admin/payment")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _userService.GetAllPayments();
            return Ok(payments);
        }
    }
}
