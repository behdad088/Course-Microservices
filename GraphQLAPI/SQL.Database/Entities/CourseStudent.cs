using System;
using System.Collections.Generic;

namespace SQL.Database.Entities
{
    public partial class CourseStudent
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? StudentId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
    }
}
