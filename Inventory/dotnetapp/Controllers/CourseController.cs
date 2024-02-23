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
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [Authorize(Roles="Admin,Student")]
        [HttpGet("course")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }
        
        [Authorize(Roles="Admin,Student")]
        [HttpGet("course/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [Authorize(Roles="Admin")]
        [HttpPost("course")]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            await _courseService.CreateCourse(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseID }, course);
        }


       [Authorize(Roles="Admin")]
[HttpPut("course/{courseId}")]
public async Task<IActionResult> UpdateCourse(int courseId, Course course)
{
    if (courseId != course.CourseID)
    {
        return BadRequest();
    }

    var existingCourse = await _courseService.GetCourseById(courseId);
    if (existingCourse == null)
    {
        return NotFound();
    }

    try
    {
        existingCourse.CourseName = course.CourseName;
        existingCourse.Description = course.Description;
        existingCourse.Duration = course.Duration;
        existingCourse.Amount = course.Amount;

        await _courseService.UpdateCourse(existingCourse);
    }
    catch (Exception)
    {
        return StatusCode(500);
    }
    return Ok(existingCourse);
}

      [Authorize(Roles="Admin")]
        [HttpDelete("course/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var isDeleted = await _courseService.DeleteCourse(id);
            if (!isDeleted)
            {
                return NotFound("Course not found.");
            }
            return Ok("Course deleted successfully.");
        }




    }
}
