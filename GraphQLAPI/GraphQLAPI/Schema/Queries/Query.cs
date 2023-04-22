using GraphQLAPI.Services.Courses;

namespace GraphQLAPI.Schema.Queries
{
    public class Query
    {
        private readonly CoursesRepository _coursesRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository ?? throw new ArgumentNullException(nameof(coursesRepository));
        }

        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            var courses = await _coursesRepository.GetAllCourseAsync();
            var courseTypes = courses.Select(x => new CourseType()
            {
                Id = x.Id,
                Name = x.Title
            });

            return courseTypes;
        }

        public async Task<CourseType?> GetCourseByIdAsync(int id)
        {
            await Task.Delay(1000);
            var course = await _coursesRepository.GetAllCourseByIdAsync(id);

            return new CourseType
            {
                Id = course.Id,
                Name = course.Title
            };
        }
    }
}
