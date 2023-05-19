namespace DeadLine.Models
{
    public class AddDiscussionDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public byte IsOpen { get; set; }
        public DateTime OpenDate { get; set; }

    }
}