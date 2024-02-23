using dotnetapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Service
{
    public interface CourseService
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourseById(int id);
        Task CreateCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(Course course);
       // bool CourseExists(int id);
    }
}
