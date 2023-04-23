using SQL.Database.Entities;

namespace GraphQLAPI.Schema.Mutations.Instructors
{
    public class InstructorResult
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Salary { get; set; }

        public virtual List<Course> Courses { get; set; }
    }
}
