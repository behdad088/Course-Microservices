using GraphQLAPI.Schema.Queries.Courses;
using HotChocolate.Data.Sorting;

namespace GraphQLAPI.Schema.Queries.Sorters
{
    public class CourseSortType : SortInputType<CourseType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(x => x.Id);
            descriptor.Ignore(x => x.InstructorId);
            base.Configure(descriptor);
        }
    }
}
