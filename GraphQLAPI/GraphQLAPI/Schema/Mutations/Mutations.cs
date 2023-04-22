using Common.SqlRepository.Entities;
using GraphQLAPI.Schema.Subscriptions;
using GraphQLAPI.Services.Courses;
using HotChocolate.Subscriptions;

namespace GraphQLAPI.Schema.Mutations
{
    public class Mutations
    {
        private readonly CoursesRepository _coursesRepository;

        public Mutations(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository ?? throw new ArgumentNullException(nameof(coursesRepository));
        }

        public async Task<CourseResult> CreateCourse(CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            var course = new Course
            {
                Title = courseInput.Title,
                Duration = courseInput.Duration,
                Level = courseInput.Level,
                PaymentType = courseInput.PaymentType,
            };

            course = await _coursesRepository.CreateCourseAsync(course);

            var courseResult = new CourseResult
            {
                Id = course.Id,
                Title = courseInput.Title,
                Duration = courseInput.Duration,
                Level = courseInput.Level,
                PaymentType = courseInput.PaymentType,
            };

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseResult);
            return courseResult;
        }

        public async Task<CourseResult> UpdateCourse(int id, CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {

            var course = new Course
            {
                Id = id,
                Title = courseInput.Title,
                Duration = courseInput.Duration,
                Level = courseInput.Level,
                PaymentType = courseInput.PaymentType,
            };

            await _coursesRepository.UpdateCourseAsync(course);
            var courseResult = new CourseResult
            {
                Id = course.Id,
                Title = courseInput.Title,
                Duration = courseInput.Duration,
                Level = courseInput.Level,
                PaymentType = courseInput.PaymentType,
            };

            var updatedCourseTopic = $"{courseResult.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updatedCourseTopic, courseResult);


            return courseResult;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var isDeleted = await _coursesRepository.DeleteCourseAsync(id);
            return isDeleted;
        }
    }
}
