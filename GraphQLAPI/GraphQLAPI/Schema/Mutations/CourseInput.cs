using GraphQLAPI.Schema.Queries;

namespace GraphQLAPI.Schema.Mutations
{
    public class CourseInput
    {
        public string Title { get; set; }
        public string Level { get; set; }
        public int Duration { get; set; }
        public string PaymentType { get; set; }
        public SubjectType SubjectType { get; set; }
    }
}
