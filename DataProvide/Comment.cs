using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Comment
    {
        public Comment()
        {
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }
        public int DiscussionId { get; set; }

        public virtual Discussion Discussion { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
