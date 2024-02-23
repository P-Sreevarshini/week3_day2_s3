using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
using dotnetapp.Services;

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
        [Autthorize(Roles="Admin,Student")]
        [HttpPost("student")]
        public IActionResult CreateUser(User user)
        {
            _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }
        [Autthorize(Roles="Student")]
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
        [Autthorize(Roles="Student")]
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

        [HttpPost("student/payment")]
        public IActionResult PostStudentPayment(long studentId, Payment payment)
        {
            payment.StudentId = studentId;
            _userService.AddPaymentToStudent(payment);
            return CreatedAtAction(nameof(PostStudentPayment), new { studentId }, payment);
        }

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
    }
}
