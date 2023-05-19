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

        [Authorize]
        [HttpPost]
        [Route("addCourse")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseDTO dto)
        {
            try
            {
                if (!canAccessProf(true))
                    return Unauthorized();

                var res = await _courseRepo.AddCourse(dto);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private bool canAccessProf(bool professor)
        {
            var claim = HttpContext.User.Claims.Where(x => x.Type == "isProfessor").FirstOrDefault();
            if (claim == null)
                return false;
            return claim.Value == professor.ToString();
        }
    }
}