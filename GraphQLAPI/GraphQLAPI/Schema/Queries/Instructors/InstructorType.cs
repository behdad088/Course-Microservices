using GraphQLAPI.Schema.Queries.Courses;

namespace GraphQLAPI.Schema.Queries.Instructors
{
    public class InstructorType
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public List<CourseType> Courses { get; set; }
    }
}
