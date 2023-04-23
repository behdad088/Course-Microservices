namespace GraphQLAPI.Schema.Mutations.Instructors
{
    public class InstructorInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Salary { get; set; }
    }
}
