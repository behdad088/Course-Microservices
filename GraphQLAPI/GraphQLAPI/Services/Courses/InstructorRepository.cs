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

        public async Task<Instructor> GetInstructorByAsync(Guid id)
        {
            var instructor = await _sql.ExecuteAsync(async db =>
            {
                return await db.Instructors
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.Id == id);
            });
            return instructor;
        }

        public async Task<IEnumerable<Instructor>> GetManyByIds(IReadOnlyList<Guid> instrcutorsIds)
        {
            var instructors = await _sql.ExecuteAsync(async db =>
            {
                return await db.Instructors.Where(x => instrcutorsIds.Contains(x.Id)).ToListAsync();
            });

            return instructors;
        }
    }
}
