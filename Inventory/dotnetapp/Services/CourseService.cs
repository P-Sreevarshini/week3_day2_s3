using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Import Entity Framework Core namespace

namespace dotnetapp.Services
{
    public class CourseService
    {
        private readonly ApplicationDbContext _context; // Inject ApplicationDbContext

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            // Return all courses from the database
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            // Find the course with the provided ID in the database
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task CreateCourse(Course course)
        {
            // Add the new course to the database
            _context.Courses.Add(course);
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        public async Task UpdateCourse(Course course)
        {
            // Update the existing course in the database
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        public async Task<bool> DeleteCourse(int courseId)
{
    var courseToRemove = await _context.Courses.FindAsync(courseId);
    if (courseToRemove != null)
    {
        _context.Courses.Remove(courseToRemove);
        await _context.SaveChangesAsync(); // Save changes to the database
        return true; // Return true if deletion was successful
    }
    return false; // Return false if course was not found
}

    }
}
