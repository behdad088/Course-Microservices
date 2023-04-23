using GraphQLAPI.Services.Courses;
using SQL.Database.Entities;

namespace GraphQLAPI.DataLoader
{
    public class InstructorDataLoader : BatchDataLoader<Guid, Instructor>
    {
        private readonly InstructorRepository _instructorRepository;

        public InstructorDataLoader(InstructorRepository instructorRepository, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _instructorRepository = instructorRepository ?? throw new ArgumentNullException(nameof(instructorRepository));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Instructor>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var instructors = await _instructorRepository.GetManyByIds(keys);
            var result = instructors.ToDictionary(x => x.Id);
            return result;
        }
    }
}
