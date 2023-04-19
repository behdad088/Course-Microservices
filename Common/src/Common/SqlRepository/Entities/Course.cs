using System;
using System.Collections.Generic;

#nullable disable

namespace Common.SqlRepository.Entities
{
    public partial class Course
    {
        public Course()
        {
            Lectures = new HashSet<Lecture>();
            Ratings = new HashSet<Rating>();
            Sections = new HashSet<Section>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public string Instructor { get; set; }
        public string PaymentType { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
