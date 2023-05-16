using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Document
    {
        public int Id { get; set; }
        public string Format { get; set; }
        public string Url { get; set; }
        public double? Grade { get; set; }
        public int DeadlineId { get; set; }
        public int StudentId { get; set; }
        public DateTime? Date { get; set; }

        public virtual Deadline Deadline { get; set; }
        public virtual Student Student { get; set; }
    }
}
