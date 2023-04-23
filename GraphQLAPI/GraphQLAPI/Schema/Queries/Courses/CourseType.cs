using GraphQLAPI.DataLoader;
using GraphQLAPI.Schema.Queries.Instructors;
using GraphQLAPI.Schema.Queries.Students;
using SQL.Database.Entities;

namespace GraphQLAPI.Schema.Queries.Courses
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public SubjectType? Subject { get; set; }

        public Guid? InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            Instructor instructor = (Instructor)(await instructorDataLoader.LoadAsync(InstructorId));

            return new InstructorType
            {
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Salary = instructor.Salary
            };
        }

        public IEnumerable<StudentType>? Students { get; set; }
    }

    public enum SubjectType
    {
        Mathematic,
        Science,
        History
    }
}
