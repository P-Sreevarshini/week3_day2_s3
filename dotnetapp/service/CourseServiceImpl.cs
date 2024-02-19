using dotnetapp.Models;
using dotnetapp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Service
{
    public class CourseServiceImpl : CourseService
    {
        private readonly CourseRepo _courseRepository;

        public CourseService(CourseRepo courseRepository)
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
