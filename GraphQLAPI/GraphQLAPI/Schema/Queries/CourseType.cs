namespace GraphQLAPI.Schema.Queries
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public SubjectType? Subject { get; set; }

        [GraphQLNonNullType]
        public InstructorType Instructor { get; set; }
        public IEnumerable<StudentType>? Students { get; set; }
    }

    public enum SubjectType
    {
        Mathematic,
        Science,
        History
    }
}
