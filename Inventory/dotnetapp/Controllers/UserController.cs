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

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.UserId }, user);
        }

        [HttpPut("{studentId}")]
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

        [HttpDelete("{studentId}")]
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

        [HttpPost("{studentId}/payments")]
        public IActionResult PostStudentPayment(long studentId, Payment payment)
        {
            // Here you can associate the payment with the student
            payment.StudentId = studentId;
            _userService.AddPaymentToStudent(payment);
            return CreatedAtAction(nameof(PostStudentPayment), new { studentId }, payment);
        }

        [HttpGet("{userId}/student")]
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
