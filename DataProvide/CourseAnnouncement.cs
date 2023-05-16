using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class CourseAnnouncement
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AnnounceDate { get; set; }
        public string Data { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
