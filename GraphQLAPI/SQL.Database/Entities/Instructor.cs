using System;
using System.Collections.Generic;

namespace SQL.Database.Entities
{
    public partial class Instructor
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Salary { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
