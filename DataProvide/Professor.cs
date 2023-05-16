using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Professor
    {
        public Professor()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
