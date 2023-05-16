using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class DeadlineAnnouncement
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AnnounceDate { get; set; }
        public int DeadlineId { get; set; }

        public virtual Deadline Deadline { get; set; }
    }
}
