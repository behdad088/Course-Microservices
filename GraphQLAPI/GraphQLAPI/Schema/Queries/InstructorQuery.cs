using GraphQLAPI.Schema.Queries.Courses;
using GraphQLAPI.Schema.Queries.Instructors;
using GraphQLAPI.Services.Courses;

namespace GraphQLAPI.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class InstructorQuery
    {
        private readonly InstructorRepository _instructorRepository;

        public InstructorQuery(InstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository ?? throw new ArgumentNullException(nameof(instructorRepository));
        }

        public async Task<InstructorType> GetInstructorById(Guid id)
        {
            var instructor = await _instructorRepository.GetInstructorByAsync(id);
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
