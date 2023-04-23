using GraphQLAPI.Schema.Queries;

namespace GraphQLAPI.Schema.Mutations
{
    public class CourseInput
    {
        public string Name { get; set; }
        public Guid InstructorId { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}
