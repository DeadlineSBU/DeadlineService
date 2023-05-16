using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class DeadlinePenalty
    {
        public int Id { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Wieght { get; set; }
        public int DeadlineId { get; set; }

        public virtual Deadline Deadline { get; set; }
    }
}
