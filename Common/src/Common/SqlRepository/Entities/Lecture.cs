using System;
using System.Collections.Generic;

#nullable disable

namespace Common.SqlRepository.Entities
{
    public partial class Lecture
    {
        public int Id { get; set; }
        public int? SectionId { get; set; }
        public int? CourseId { get; set; }
        public int? SeqNo { get; set; }
        public string Name { get; set; }
        public string MediaUrl { get; set; }
        public string Instructions { get; set; }
        public string Questions { get; set; }
        public string Discriminator { get; set; }

        public virtual Course Course { get; set; }
        public virtual Section Section { get; set; }
    }
}
