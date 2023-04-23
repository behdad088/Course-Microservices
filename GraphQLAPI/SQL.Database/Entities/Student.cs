using System;
using System.Collections.Generic;

namespace SQL.Database.Entities
{
    public partial class Student
    {
        public Student()
        {
            CourseStudents = new HashSet<CourseStudent>();
            Ratings = new HashSet<Rating>();
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Gpa { get; set; }

        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
