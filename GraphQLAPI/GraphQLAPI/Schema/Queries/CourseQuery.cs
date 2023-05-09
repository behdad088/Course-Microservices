using GraphQLAPI.Schema.Queries.Courses;
using GraphQLAPI.Schema.Queries.Filters;
using GraphQLAPI.Schema.Queries.Sorters;
using GraphQLAPI.Services.Courses;

namespace GraphQLAPI.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class CourseQuery
    {
        private readonly CoursesRepository _coursesRepository;

        public CourseQuery(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository ?? throw new ArgumentNullException(nameof(coursesRepository));
        }



        // Here we should get the DB context diretly to return IQueryable instead of 
        // getting all the rows. IQueryable will allow Hot Chocolate to pick 
        // only the requested rows form DB. 
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering(typeof(CourseFilterType))]
        [UseSorting(typeof(CourseSortType))]
        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            var courses = await _coursesRepository.GetAllCourseAsync();
            var courseTypes = courses.Select(x => new CourseType()
            {
                Id = x.Id,
                Name = x.Name,
                Subject = Enum.TryParse<SubjectType>(x.Subject, out var enumValue) ? enumValue : null,
                InstructorId = x.InstructorId,
                CreatorId = x.CreatorId
            });

            return courseTypes;
        }

        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        public async Task<IEnumerable<CourseType>> GetOffsetCourses()
        {
            var courses = await _coursesRepository.GetAllCourseAsync();
            var courseTypes = courses.Select(x => new CourseType()
            {
                Id = x.Id,
                Name = x.Name,
                Subject = Enum.TryParse<SubjectType>(x.Subject, out var enumValue) ? enumValue : null,
                InstructorId = x.InstructorId,
            });

            return courseTypes;
        }

        public async Task<CourseType?> GetCourseByIdAsync(Guid id)
        {
            var course = await _coursesRepository.GetCourseByIdAsync(id);

            return new CourseType
            {
                Id = course.Id,
                Name = course.Name,
                InstructorId = course.InstructorId,
                Subject = Enum.TryParse<SubjectType>(course.Subject, out var enumValue) ? enumValue : null,
                CreatorId = course.CreatorId
            };
        }
    }
}
