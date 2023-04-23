using SQL.Database.Entities;

namespace GraphQLAPI.Schema.Mutations
{
    public class CourseResult
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual List<CourseStudent> CourseStudents { get; set; }
        public virtual List<Rating> Ratings { get; set; }
    }
}
