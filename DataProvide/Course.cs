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
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? MaxSize { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ProfessorId { get; set; }
        public int CourseStatus { get; set; }
        public string ShareId { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CourseAnnouncement> CourseAnnouncements { get; set; }
        public virtual ICollection<Deadline> Deadlines { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
