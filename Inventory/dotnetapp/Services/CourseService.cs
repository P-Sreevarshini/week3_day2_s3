using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class CourseService
    {
        private readonly List<Course> _courses;

        public CourseService()
        {
            // Initialize an empty list of courses
            _courses = new List<Course>();
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            // Return all courses in the list
            return _courses;
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            // Find the course with the provided ID in the list
            return _courses.FirstOrDefault(c => c.CourseID == courseId);
        }

        public async Task CreateCourse(Course course)
        {
            // Add the new course to the list
            _courses.Add(course);
        }

        public async Task UpdateCourse(Course course)
        {
            // Find the existing course in the list
            var existingCourse = _courses.FirstOrDefault(c => c.CourseID == course.CourseID);
            if (existingCourse != null)
            {
                // Update the existing course's properties
                existingCourse.CourseName = course.CourseName;
                existingCourse.Description = course.Description;
                existingCourse.Duration = course.Duration;
                existingCourse.Amount = course.Amount;
            }
        }

        public async Task DeleteCourse(int courseId)
        {
            // Find the course with the provided ID in the list
            var courseToRemove = _courses.FirstOrDefault(c => c.CourseID == courseId);
            if (courseToRemove != null)
            {
                // Remove the course from the list
                _courses.Remove(courseToRemove);
            }
        }
    }
}
