using GraphQLAPI.Schema.Queries.Courses;

namespace GraphQLAPI.Schema.Mutations.Courses
{
    public class CourseInput
    {
        public string Name { get; set; }
        public Guid InstructorId { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}
