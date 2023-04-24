using GraphQLAPI.Schema.Queries.Courses;
using HotChocolate.Data.Filters;

namespace GraphQLAPI.Schema.Queries.Filters
{
    public class CourseFilterType : FilterInputType<CourseType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(c => c.Students);
            base.Configure(descriptor);
        }
    }
}
