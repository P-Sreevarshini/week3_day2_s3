using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Repositories
{
    public class CourseRepo : ICourseRepo
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses;
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        // public bool CourseExists(int id)
        // {
        //     return _context.Courses.Any(e => e.CourseID == id);
        // }
    }
}
