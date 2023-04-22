using Common.SqlRepository;
using Common.SqlRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAPI.Services.Courses
{
    public class CoursesRepository
    {
        private readonly ISqlFactory _sql;

        public CoursesRepository(ISqlFactory sql)
        {
            _sql = sql ?? throw new ArgumentNullException(nameof(sql));
        }

        public async Task<List<Course>> GetAllCourseAsync()
        {
            var courses = await _sql.ExecuteAsync(async db =>
            {
                return await db.Courses.ToListAsync();
            });
            return courses;
        }

        public async Task<Course> GetAllCourseByIdAsync(int Id)
        {
            var course = await _sql.ExecuteAsync(async db =>
            {
                return await db.Courses.FirstOrDefaultAsync(x => x.Id == Id);
            });
            return course;
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            var addedCourse = await _sql.AddItemAsync(course);
            return addedCourse;
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            var updatedCourse = await _sql.UpdateItemAsync(course);
            return updatedCourse;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = _sql.Execute(db =>
            {

                var course = db.Courses.FirstOrDefault(x => x.Id == id);

                if (course == null)
                    throw new GraphQLException(new Error($"Course with id {id} not found.", "COURSE_NOT_FOUND"));

                return course;
            });

            await _sql.DeleteItemAsync(course);

            return true;
        }
    }
}
