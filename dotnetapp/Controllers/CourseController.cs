using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Service;
using dotnetapp.Repository;
using System;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            await _courseService.CreateCourse(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseID }, course);
        }

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
        await _courseService.UpdateCourse(course);
    }
    catch (Exception)
    {
        return StatusCode(500);
    }

    // Get the updated course
    var updatedCourse = await _courseService.GetCourseById(CourseID);

    return Ok(updatedCourse);
}


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
