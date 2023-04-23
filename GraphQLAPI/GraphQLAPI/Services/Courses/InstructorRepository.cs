using Microsoft.EntityFrameworkCore;
using SQL.Database;
using SQL.Database.Entities;

namespace GraphQLAPI.Services.Courses
{
    public class InstructorRepository
    {
        private readonly IDatabaseFactory _sql;

        public InstructorRepository(IDatabaseFactory sql)
        {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
        }

        public async Task<Instructor> CreateInstructorAsync(Instructor instructor)
        {
            instructor.Id = Guid.NewGuid();
            var addedInstructor = await _sql.AddItemAsync(instructor);
            return addedInstructor;
        }

        public async Task<Instructor> GetInstructorAsync(Guid id)
        {
            var instructor = await _sql.ExecuteAsync(async db =>
            {
                return await db.Instructors
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.Id == id);
            });
            return instructor;
        }
    }
}
