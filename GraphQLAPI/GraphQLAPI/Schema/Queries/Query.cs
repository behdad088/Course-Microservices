using GraphQLAPI.Services.Courses;

namespace GraphQLAPI.Schema.Queries
{
    public class Query
    {
        private readonly CoursesRepository _coursesRepository;
        private readonly InstructorRepository _instructorRepository;

        public Query(CoursesRepository coursesRepository, InstructorRepository instructorRepository)
        {
            _coursesRepository = coursesRepository ?? throw new ArgumentNullException(nameof(coursesRepository));
            _instructorRepository = instructorRepository ?? throw new ArgumentNullException(nameof(instructorRepository));
        }

        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            var courses = await _coursesRepository.GetAllCourseAsync();
            var courseTypes = courses.Select(x => new CourseType()
            {
                Id = x.Id,
                Name = x.Name,
                Instructor = new InstructorType()
                {
                    Id = x.Instructor.Id,
                    FirstName = x.Instructor.FirstName,
                    LastName = x.Instructor.LastName,
                    Salary = x.Instructor.Salary
                }
            });

            return courseTypes;
        }

        public async Task<CourseType?> GetCourseByIdAsync(Guid id)
        {
            var course = await _coursesRepository.GetCourseByIdAsync(id);

            return new CourseType
            {
                Id = course.Id,
                Name = course.Name,
                Instructor = new InstructorType()
                {
                    Id = course.Instructor.Id,
                    FirstName = course.Instructor.FirstName,
                    LastName = course.Instructor.LastName,
                    Salary = course.Instructor.Salary
                }
            };
        }

        public async Task<InstructorType> GetInstructorById(Guid id)
        {
            var instructor = await _instructorRepository.GetInstructorAsync(id);
            return new InstructorType
            {
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Salary = instructor.Salary,
                Courses = instructor.Courses.Select(x => new CourseType
                {
                    Id = x.Id,
                    Name = x.Name,
                    Subject = Enum.TryParse<SubjectType>(x.Subject, out var enumValue) ? enumValue : null
                }).ToList()
            };
        }
    }
}
