using dotnetapp.Models;
using dotnetapp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class CourseServiceImpl : CourseService
    {
        private readonly ICourseRepo _courseRepository;

        public CourseService(ICourseRepo courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAllCourses();
        }

        public Course GetCourseById(int id)
        {
            return _courseRepository.GetCourseById(id);
        }

        public async Task CreateCourse(Course course)
        {
            await _courseRepository.CreateCourse(course);
        }

        public async Task UpdateCourse(Course course)
        {
            await _courseRepository.UpdateCourse(course);
        }

        public async Task DeleteCourse(Course course)
        {
            await _courseRepository.DeleteCourse(course);
        }

        // public bool CourseExists(int id)
        // {
        //     return _courseRepository.CourseExists(id);
        // }
    }
}
