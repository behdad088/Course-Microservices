using GraphQLAPI.Schema.Mutations.Courses;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLAPI.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            var topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";
            return topicEventReceiver.SubscribeAsync<CourseResult>(topicName);
        }
    }
}
