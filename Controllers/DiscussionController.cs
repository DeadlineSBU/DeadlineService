using Deadline.redis;
using DeadLine.Models;
using DeadLine.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DeadLine.DataProvide;

namespace DeadLine.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRedisCache _redisCache;
        private readonly IConfiguration _configuration;

        private readonly IDiscussionRepo _discussionRepo;

        public DiscussionController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IRedisCache redisCache,
            IHttpClientFactory client,
            IDiscussionRepo discussionRepo
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _redisCache = redisCache;
            _discussionRepo = discussionRepo;
        }

        [HttpPost]
        [Route("AddDiscussion")]
        public async Task<IActionResult> AddDiscussion(AddDiscussionDTO dto)
        {
            try
            {
                if (!canAccessProf(true))
                    return Unauthorized();
                var userId = getUserId();

                var res = await _discussionRepo.AddDiscussion(userId, dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDiscussionById/{id}")]
        public async Task<IActionResult> GetDiscussion(int id)
        {
            try
            {
                var userId = getUserId();

                var res = await _discussionRepo.GetDiscussion(userId, id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListenOnDiscussion/{id}")]
        public async Task<IActionResult> ListenOnDiscussion(int id)
        {
            try
            {
                var userId = getUserId();

                var res = await _discussionRepo.ListenOnDiscussion(userId, id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddComment/{id}")]
        public async Task<IActionResult> AddComment(int id, CommentDTO dto)
        {
            try
            {
                var userId = getUserId();

                var res = await _discussionRepo.AddComment(userId, id, dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ReplyOnComment/{id}")]
        public async Task<IActionResult> ReplyOnComment(int id, CommentReplyDTO dto)
        {
            try
            {
                var userId = getUserId();

                var res = await _discussionRepo.ReplyOnComment(userId, id, dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProfessorDiscussions")]
        public async Task<IActionResult> GetProfessorDiscussions()
        {
            try
            {
                if (!canAccessProf(true))
                    return Unauthorized();
                var userId = getUserId();

                var res = await _discussionRepo.GetProfessorDiscussions(userId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetStudentDiscussions")]
        public async Task<IActionResult> GetStudentDiscussions()
        {
            try
            {
                if (!canAccessProf(false))
                    return Unauthorized();
                var userId = getUserId();

                var res = await _discussionRepo.GetStudentDiscussions(userId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        private bool canAccessProf(bool professor)
        {
            var claim = HttpContext.User.Claims
                .Where(x => x.Type == "isProfessor")
                .FirstOrDefault();
            if (claim == null)
                return false;
            return claim.Value == professor.ToString();
        }

        private string getUserId()
        {
            var claim = HttpContext.User.Claims.Where(x => x.Type == "user_id").FirstOrDefault();
            if (claim == null)
                return null;
            return claim.Value;
        }
    }
}
