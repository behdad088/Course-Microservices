using GraphQLAPI.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLAPI.Schema.Mutations
{
    public class Mutations
    {
        private readonly List<CourseResult> _courses;

        public Mutations()
        {
            _courses = new List<CourseResult>();
        }

        public async Task<CourseResult> CreateCourse(CourseInput CourseInput, [Service] ITopicEventSender topicEventSender)
        {
            var course = new CourseResult
            {
                Id = Guid.NewGuid(),
                Name = CourseInput.Name,
                SubjectType = CourseInput.SubjectType,
                InstructorId = CourseInput.InstructorId,
            };

            _courses.Add(course);
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
            return course;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInput CourseInput, [Service] ITopicEventSender topicEventSender)
        {
            var currentCourse = _courses.FirstOrDefault(c => c.Id == id);

            if (currentCourse == null)
                throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));

            currentCourse.Name = CourseInput.Name;
            currentCourse.SubjectType = CourseInput.SubjectType;
            currentCourse.InstructorId = CourseInput.InstructorId;

            var updatedCourseTopic = $"{currentCourse.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updatedCourseTopic, currentCourse);


            return currentCourse;
        }

        public bool DeleteCourse(Guid id)
        {
            var course = _courses.Where(x => x.Id == id).FirstOrDefault();

            if (course == null)
                throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));

            var isDeleted = _courses.Remove(course);
            return isDeleted;
        }
    }
}
