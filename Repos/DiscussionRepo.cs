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

        public Task<object> AddDiscussion(string professorId, AddDiscussionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDiscussion(string userId, int id)
        {
            throw new NotImplementedException();
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
