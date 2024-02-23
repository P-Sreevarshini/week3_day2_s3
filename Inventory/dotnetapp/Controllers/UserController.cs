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
        public IActionResult GetUserById(long userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [Authorize(Roles="Admin,Student")]
        [HttpPost("student")]
        public IActionResult CreateUser(User user)
        {
            _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }
        [Authorize(Roles="Student")]
        [HttpPut("student/{id}")]
        public IActionResult UpdateUser(long studentId, User user)
        {
            if (studentId != user.UserId)
            {
                return BadRequest();
            }

            var existingUser = _userService.GetUserById(studentId);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userService.UpdateUser(user);

            return NoContent();
        }
        [Authorize(Roles="Student")]
        [HttpDelete("student/{id}")]
        public IActionResult DeleteUser(long studentId)
        {
            var existingUser = _userService.GetUserById(studentId);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(studentId);

            return NoContent();
        }
        [Authorize(Roles="Student")]
        [HttpPost("student/payment")]
        public IActionResult PostStudentPayment(long studentId, Payment payment)
        {
            payment.StudentId = studentId;
            _userService.AddPaymentToStudent(payment);
            return CreatedAtAction(nameof(PostStudentPayment), new { studentId }, payment);
        }
        [Authorize(Roles="Admin,Student")]
        [HttpGet("student/user/{userId}")]
        public IActionResult GetStudentByUserId(long userId)
        {
            var student = _userService.GetStudentByUserId(userId);
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
            var payments = await _paymentService.GetAllPayments();
            return Ok(payments);
        }

    }
}
