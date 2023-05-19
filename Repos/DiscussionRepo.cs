using DeadLine.DataProvide;
using DeadLine.Models;

namespace DeadLine.Repos
{
    public class DiscussionRepo : IDiscussionRepo
    {
        private readonly DeadlineContext _context;
        private readonly ApplicationDbContext _userContext;

        public DiscussionRepo(DeadlineContext context, ApplicationDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public Task<object> AddComment(string userId, int id, CommentDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<object> AddDiscussion(string professorId, AddDiscussionDTO dto)
        {
            var course = _context.Courses
                .Where(c => c.ProfessorId == professorId && c.Id == dto.CourseId)
                .FirstOrDefault();
            if (course == null)
                throw new InvalidOperationException("not permitted");
            var discussion = _context.Discussions.Where(
                d => d.Title == dto.Title && d.CourseId == dto.CourseId
            ).FirstOrDefault();
            if (discussion != null)
                throw new InvalidOperationException("is already created");

            await _context.Discussions.AddAsync(
                new Discussion
                {
                    Title = dto.Title,
                    OpenDate = dto.OpenDate,
                    CreatedDate = DateTime.Now,
                    IsOpen = dto.IsOpen,
                    CourseId = dto.CourseId
                }
            );

            await _context.SaveChangesAsync();
            return new { message = "Added Sucessfully" };
        }

        public async Task<object> GetDiscussion(string userId, int id)
        {
            var discussion = _context.Discussions.Where(d => d.Id == id).FirstOrDefault();
            if (discussion == null)
                throw new InvalidDataException("discussion not founded");
            var course = _context.Courses
                .Where(c => c.ProfessorId == userId && discussion.CourseId == c.Id)
                .FirstOrDefault();
            if (course == null)
                throw new InvalidOperationException("not permitted");

            var data = (
                from comment in _context.Comments.AsEnumerable()
                where comment.DiscussionId == id
                join reply in _context.Replies.AsEnumerable()
                    on comment.Id equals reply.CommentId
                    into commentReplies
                select new
                {
                    Date = comment.Date,
                    Owner = comment.UserId,
                    CommentId = comment.Id,
                    Value = comment.Value,
                    Replies = (
                        from reply in commentReplies
                        select new
                        {
                            Date = reply.Date,
                            Owner = reply.UserId,
                            ReplyId = reply.Id,
                            Value = reply.Value,
                        }
                    ).ToList()
                }
            ).ToList();

            return data;
        }

        public Task<object> GetProfessorDiscussions(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetStudentDiscussions(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<object> ListenOnDiscussion(string userId, int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> ReplyOnComment(string userId, int id, CommentReplyDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
