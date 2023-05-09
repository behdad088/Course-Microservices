using GraphQLAPI.DataLoader;
using GraphQLAPI.Schema.Queries.Instructors;
using GraphQLAPI.Schema.Queries.Students;
using GraphQLAPI.Schema.Queries.Users;
using SQL.Database.Entities;

namespace GraphQLAPI.Schema.Queries.Courses
{
    public class CourseType : ISearchResultType
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public SubjectType? Subject { get; set; }

        [IsProjected(true)]
        public Guid? InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            Instructor instructor = (Instructor)await instructorDataLoader.LoadAsync(InstructorId);

            return new InstructorType
            {
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Salary = instructor.Salary
            };
        }

        public IEnumerable<StudentType>? Students { get; set; }

        public async Task<UserType?> Creator([Service] UserDataLoader userDataLoader)
        {
            if (string.IsNullOrEmpty(CreatorId))
                return null;

            var user = await userDataLoader.LoadAsync(CreatorId);
            return user;
        }

        [IsProjected(true)]
        public string? CreatorId { get; set; }
    }

    public enum SubjectType
    {
        Mathematic,
        Science,
        History
    }
}
