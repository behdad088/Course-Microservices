using GraphQLAPI.Schema.Queries;

namespace GraphQLAPI.Schema.Mutations
{
    public class CourseResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public int Duration { get; set; }
        public string PaymentType { get; set; }
        public SubjectType SubjectType { get; set; }
        public Guid InstructorId { get; set; }
    }
}
