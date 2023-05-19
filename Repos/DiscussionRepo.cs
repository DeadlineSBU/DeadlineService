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

       
    }
}
