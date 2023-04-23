using GraphQLAPI.Schema.Subscriptions;
using GraphQLAPI.Services.Courses;
using HotChocolate.Subscriptions;
using SQL.Database.Entities;

namespace GraphQLAPI.Schema.Mutations
{
    public class Mutations
    {
        private readonly CoursesRepository _coursesRepository;
        private readonly InstructorRepository _instructorRepository;

        public Mutations(CoursesRepository coursesRepository, InstructorRepository instructorRepository)
        {
            _coursesRepository = coursesRepository ?? throw new ArgumentNullException(nameof(coursesRepository));
            _instructorRepository = instructorRepository ?? throw new ArgumentNullException(nameof(instructorRepository));
        }

        public async Task<CourseResult> CreateCourse(CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            var course = new Course
            {
                Name = courseInput.Name,
                Subject = courseInput.SubjectType.ToString(),
                InstructorId = courseInput.InstructorId
            };

            course = await _coursesRepository.CreateCourseAsync(course);
            var courseResult = new CourseResult
            {
                Id = course.Id,
                Name = course.Name,
                Subject = course.Subject,
                Instructor = course.Instructor,
                Ratings = course.Ratings.ToList(),
            };

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseResult);
            return courseResult;
        }

        public async Task<InstructorResult> CreateInstructor(InstructorInput instructorInput)
        {
            var instructor = new Instructor
            {
                FirstName = instructorInput.FirstName,
                LastName = instructorInput.LastName,
                Salary = instructorInput.Salary
            };

            var addedInstructor = await _instructorRepository.CreateInstructorAsync(instructor);

            var courseResult = new InstructorResult
            {
                Id = addedInstructor.Id,
                FirstName = addedInstructor.FirstName,
                LastName = addedInstructor.LastName,
                Salary = addedInstructor.Salary,
                Courses = addedInstructor.Courses.ToList()
            };

            return courseResult;
        }

        public async Task<CourseResult> UpdateCourse(int id, CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            var course = new Course
            {
                Name = courseInput.Name,
                Subject = courseInput.SubjectType.ToString(),
                InstructorId = courseInput.InstructorId
            };

            await _coursesRepository.UpdateCourseAsync(course);
            var courseResult = new CourseResult
            {
                Id = course.Id,
                Name = course.Name,
                Subject = course.Subject,
                Instructor = course.Instructor,
                Ratings = course.Ratings.ToList(),
            };

            var updatedCourseTopic = $"{courseResult.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updatedCourseTopic, courseResult);


            return courseResult;
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var isDeleted = await _coursesRepository.DeleteCourseAsync(id);
            return isDeleted;
        }
    }
}
