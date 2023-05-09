using GraphQLAPI.Schema.Queries.Courses;
using GraphQLAPI.Schema.Queries.Instructors;
using SQL.Database;

namespace GraphQLAPI.Schema.Queries
{
    public class Query
    {
        private readonly IDatabaseFactory _databaseFactory;

        public Query(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        }

        public async Task<IEnumerable<ISearchResultType>> SearchAsync(string term)
        {
            using var context = _databaseFactory.GetCourseDbContext();
            var courseType = context.Courses.Where(x => x.Name != null && x.Name.Contains(term))
                .ToList()
                .Select(x => new CourseType()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Subject = Enum.TryParse<SubjectType>(x.Subject, out var enumValue) ? enumValue : null,
                    InstructorId = x.InstructorId,
                    CreatorId = x.CreatorId
                }).ToList();

            var instructionType = context.Instructors
                .Where(x => (x.FirstName != null && x.FirstName.Contains(term)) || (x.LastName != null && x.LastName.Contains(term)))
                .Select(x => new InstructorType
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Salary = x.Salary
                }).ToList();

            return new List<ISearchResultType>().Concat(courseType).Concat(instructionType);
        }
    }
}

