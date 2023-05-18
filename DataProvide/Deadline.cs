using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Deadline
    {
        public Deadline()
        {
            DeadlineAnnouncements = new HashSet<DeadlineAnnouncement>();
            DeadlinePenalties = new HashSet<DeadlinePenalty>();
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string FileFormats { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<DeadlineAnnouncement> DeadlineAnnouncements { get; set; }
        public virtual ICollection<DeadlinePenalty> DeadlinePenalties { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
