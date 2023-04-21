using Bogus;

namespace GraphQLAPI.Schema.Queries
{
    public class Query
    {
        private readonly Faker<InstructorType> _instructorFaker;
        private readonly Faker<StudentType> _studentfaker;
        private readonly Faker<CourseType> _courseFaker;
        private readonly List<CourseType> _courses = new List<CourseType>();
        public Query()
        {
            _instructorFaker = new Faker<InstructorType>()
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Salary, f => f.Random.Double(10000, 40000));

            _studentfaker = new Faker<StudentType>()
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.GPA, f => f.Random.Double(1, 4));

            _courseFaker = new Faker<CourseType>()
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.Name, x => x.Name.JobTitle())
                .RuleFor(x => x.Subject, x => x.PickRandom<SubjectType>())
                .RuleFor(x => x.Instructor, x => _instructorFaker.Generate())
                .RuleFor(x => x.Students, x => _studentfaker.Generate(3));

            _courses.AddRange(_courseFaker.Generate(5));
        }


        public IEnumerable<CourseType> GetCourse()
        {
            return _courses;
        }

        public async Task<CourseType?> GetCourseByIdAsync(Guid id)
        {
            await Task.Delay(1000);
            var course = _courses.FirstOrDefault(x => x.Id == id);
            return course;
        }
    }
}
