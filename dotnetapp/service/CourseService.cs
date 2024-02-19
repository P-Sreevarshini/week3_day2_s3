using dotnetapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public interface CourseService
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int id);
        Task CreateCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(Course course);
       // bool CourseExists(int id);
    }
}
