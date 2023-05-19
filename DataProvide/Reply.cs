using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Reply
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int CommentId { get; set; }
        public string UserId { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
