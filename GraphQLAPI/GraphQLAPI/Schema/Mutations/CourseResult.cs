using GraphQLAPI.Schema.Queries;

namespace GraphQLAPI.Schema.Mutations
{
    public class CourseResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubjectType SubjectType { get; set; }
        public Guid InstructorId { get; set; }
    }
}
