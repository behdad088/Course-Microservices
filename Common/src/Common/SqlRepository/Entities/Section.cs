using System;
using System.Collections.Generic;

#nullable disable

namespace Common.SqlRepository.Entities
{
    public partial class Section
    {
        public Section()
        {
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SeqNo { get; set; }
        public string Title { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
