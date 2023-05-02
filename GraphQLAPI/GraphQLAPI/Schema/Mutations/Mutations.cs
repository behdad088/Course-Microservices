using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQLAPI.Schema.Mutations.Courses;
using GraphQLAPI.Schema.Mutations.Instructors;
using GraphQLAPI.Schema.Subscriptions;
using GraphQLAPI.Services.Courses;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using SQL.Database.Entities;
using System.Security.Claims;

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

        [Authorize]
        public async Task<CourseResult> CreateCourse(CourseInput courseInput,
            [Service] ITopicEventSender topicEventSender,
            ClaimsPrincipal claimsPrincipal)
        {

            var userId = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID);

            var course = new Course
            {
                Name = courseInput.Name,
                Subject = courseInput.SubjectType.ToString(),
                InstructorId = courseInput.InstructorId,
                CreatorId = userId
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

        [Authorize]
        public async Task<CourseResult> UpdateCourse(Guid id, CourseInput courseInput,
            [Service] ITopicEventSender topicEventSender, ClaimsPrincipal claimsPrincipal)
        {
            var dbCourse = await _coursesRepository.GetCourseByIdAsync(id);
            var userId = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID);

            if (dbCourse == null)
                throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));

            if (userId != dbCourse.CreatorId)
                throw new GraphQLException(new Error("You do not have permission to update this course.", "INVALID_PERMISSION"));

            dbCourse.Name = courseInput.Name;
            dbCourse.Subject = courseInput.SubjectType.ToString();
            dbCourse.InstructorId = courseInput.InstructorId;

            await _coursesRepository.UpdateCourseAsync(dbCourse);
            var courseResult = new CourseResult
            {
                Id = dbCourse.Id,
                Name = dbCourse.Name,
                Subject = dbCourse.Subject,
                Instructor = dbCourse.Instructor,
                Ratings = dbCourse.Ratings.ToList(),
            };

            var updatedCourseTopic = $"{courseResult.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updatedCourseTopic, courseResult);


            return courseResult;
        }

        [Authorize(Policy = "IsAdmin")]
        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var isDeleted = await _coursesRepository.DeleteCourseAsync(id);
            return isDeleted;
        }
    }
}
