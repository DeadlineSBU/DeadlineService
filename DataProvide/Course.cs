using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Course
    {
        public Course()
        {
            CourseAnnouncements = new HashSet<CourseAnnouncement>();
            Deadlines = new HashSet<Deadline>();
            Discussions = new HashSet<Discussion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? MaxSize { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ProfessorId { get; set; }
        public int CourseStatusId { get; set; }
        public string ShareId { get; set; }
        public string PasswordHash { get; set; }

        public virtual CourseStatus CourseStatus { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual ICollection<CourseAnnouncement> CourseAnnouncements { get; set; }
        public virtual ICollection<Deadline> Deadlines { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
    }
}
