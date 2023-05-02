using SQL.Database.Entities;

namespace GraphQLAPI.Schema.Mutations.Courses
{
    public class CourseResult
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? CreatorId { get; set; }

        public Instructor? Instructor { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
