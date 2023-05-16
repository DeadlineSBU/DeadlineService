using System;
using System.Collections.Generic;

#nullable disable

namespace DeadLine.DataProvide
{
    public partial class Student
    {
        public Student()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
