using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Service;
using dotnetapp.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }
      [AllowAnonymous]
        [HttpGet("{CourseId}")]
        public async Task<IActionResult> GetCourseById(int CourseId)
        {
            var course = await _courseService.GetCourseById(CourseId);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [Authorize(Roles="Admin")]
       [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            await _courseService.CreateCourse(course);
            return CreatedAtAction(nameof(GetCourseById), new { CourseId = course.CourseID }, course);
        }

[Authorize(Roles="Admin")]
[HttpPut("{CourseID}")]
public async Task<IActionResult> UpdateCourse(int CourseID, Course course)
{
    if (CourseID != course.CourseID)
    {
        return BadRequest();
    }

    var existingCourse = await _courseService.GetCourseById(CourseID);
    if (existingCourse == null)
    {
        return NotFound();
    }

    try
    {
        // Update the existing course with the properties of the provided course
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

    // Return the updated course
    return Ok(existingCourse);
}


      [Authorize(Roles="Admin")]
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteCourse(int id)
{
    var course = await _courseService.GetCourseById(id);
    if (course == null)
    {
        return NotFound();
    }

    await _courseService.DeleteCourse(course);
    return NoContent();
}

    }
}