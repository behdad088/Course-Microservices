namespace SQL.Database.Entities
{
    public partial class Course
    {
        public Course()
        {
            CourseStudents = new HashSet<CourseStudent>();
            Ratings = new HashSet<Rating>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public Guid? InstructorId { get; set; }
        public string? CreatorId { get; set; }

        public virtual Instructor? Instructor { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
