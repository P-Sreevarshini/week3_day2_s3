using dotnetapp.Models;
using dotnetapp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetapp.Service
{
    public class CourseServiceImpl : CourseService
    {
        private readonly CourseRepo _courseRepository;

        public CourseServiceImpl(CourseRepo courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _courseRepository.GetAllCourses();
        }

        public async Task<Course> GetCourseById(int id)
        {
            return await _courseRepository.GetCourseById(id);
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
    }
}
