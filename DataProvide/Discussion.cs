using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Discussion
    {
        public Discussion()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public byte IsOpen { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
