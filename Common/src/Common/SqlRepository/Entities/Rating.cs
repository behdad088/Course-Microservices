using System;
using System.Collections.Generic;

#nullable disable

namespace Common.SqlRepository.Entities
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string StudentName { get; set; }
        public byte StarValue { get; set; }
        public string Review { get; set; }

        public virtual Course Course { get; set; }
    }
}
