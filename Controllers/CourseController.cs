using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DeadLine.DataProvide;
using DeadLine.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Newtonsoft.Json;
using Deadline.redis;
using DeadLine.Repos;

namespace DeadLine.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRedisCache _redisCache;
        private readonly IConfiguration _configuration;

        private readonly ICourseRepo _courseRepo;

        public CourseController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IRedisCache redisCache,
            IHttpClientFactory client,
            ICourseRepo courseRepo
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _redisCache = redisCache;
            _courseRepo = courseRepo;
        }

        [HttpPost]
        [Route("addCourse")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseDTO dto)
        {
            try
            {
                if (!canAccessProf(true))
                    return Unauthorized();
                var userId = getUserId();

                var res = await _courseRepo.AddCourse(userId,dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getCourseById/{id}")]
        public async Task<IActionResult> GetCourse( int id)
        {
            System.Console.WriteLine(id);
            try
            {
                var userId = getUserId();

                var res = await _courseRepo.GetCourse(userId,id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // public async Task<IActionResult> JoinCourse(int studentId, JoinCourseDTO dto);
        // public async Task<IActionResult> GetStudents(int id);

        // public async Task<IActionResult> GetDeadlines(int id);
        // public async Task<IActionResult> GetDiscussions(int id);
        // public async Task<IActionResult> GetProfessorCourses(int id);
        // public async Task<IActionResult> GetStudentCourses(int id);


        private bool canAccessProf(bool professor)
        {
            var claim = HttpContext.User.Claims.Where(x => x.Type == "isProfessor").FirstOrDefault();
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