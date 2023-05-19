using DeadLine.Models;
namespace DeadLine.Repos
{
    public interface IDiscussionRepo
    {
        public Task<object> AddDiscussion(string professorId, AddDiscussionDTO dto);
        public Task<object> ListenOnDiscussion(string userId, int id);
        public Task<object> AddComment(string userId, int id, CommentDTO dto);
        public Task<object> ReplyOnComment(string userId, int id, CommentReplyDTO dto);

        public Task<object> GetDiscussion(string userId, int id);
        public Task<object> GetProfessorDiscussions(string userId);
        public Task<object> GetStudentDiscussions(string userId);


    }
}