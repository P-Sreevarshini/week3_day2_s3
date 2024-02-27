using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CourseEnquiryDbContext _context;

        public StudentController(CourseEnquiryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet("course")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllStudentCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses);
        }
    }
}