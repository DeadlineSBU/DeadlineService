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

namespace DeadLine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRedisCache _redisCache;

        private readonly IConfiguration _configuration;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context,
            IRedisCache redisCache,
            IHttpClientFactory client
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _redisCache = redisCache;

        }



        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && user.IsProfessor && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim("user_id",user.Id as string),
                    new Claim("username", user.UserName as string),
                    new Claim("isProfessor",user.IsProfessor.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                    var token = CreateToken(authClaims);
                    var refreshToken = GenerateRefreshToken();

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                    await _userManager.UpdateAsync(user);
                    return Ok(new
                    {
                        access_token = new JwtSecurityTokenHandler().WriteToken(token),
                        refresh_token = refreshToken,
                        Expiration = token.ValidTo
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }




        [HttpPost]
        [Route("checkForRegister")]
        public async Task<IActionResult> CheckForRegisterProfessor([FromBody] RegisterModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);

                if (userExists != null)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

                }
                var mobileExists = _context.Users.Where(u => u.PhoneNumber == model.MobileNumber).FirstOrDefault();
                if (mobileExists != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "mobile number already exists!" });
                }




                var random = new Random();
                var randomNumber = "";
                randomNumber = random.Next(1000, 9999).ToString();

                var param = new RedisModel
                {
                    MobileNumber = model.MobileNumber,
                    Message = "1234",
                    FirstName = model.FirstName,
                    IsProfessor = model.IsProfessor,
                    LastName = model.LastName,
                    Password = model.Password,
                    Username = model.Username
                };


                /*var sms = new Ghasedak.Core.Api(SMSToken);

                var result = await sms.SendSMSAsync(dto.message, dto.mobile, linenumber: LineNumber);

                if (result.Result.Code != 200)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Sms not sent!" });

                }*/


                //adding sms to redis by mobile and message
                await _redisCache.SetAsync(model.MobileNumber, param, 5);

                return Ok(new { Status = "Success", Message = "Register is OK! OTP Sent" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] VerifySmsModel smsModel)
        {
            try
            {
                var model = await _redisCache.GetAsync(smsModel.mobileNumber);
                if (model == null || model.Message != smsModel.verificationCode) return BadRequest("Invalid code!");



                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

                }

                ApplicationUser user = new()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    IsProfessor = model.IsProfessor,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.MobileNumber,

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
                }

                return Ok(new { Status = "Success", Message = "User created successfully!" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            try
            {
                if (tokenModel is null)
                {
                    return BadRequest("Invalid client request");
                }

                string? accessToken = tokenModel.AccessToken;
                string? refreshToken = tokenModel.RefreshToken;

                var principal = GetPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    return BadRequest("Invalid access token or refresh token");
                }


                string username = principal.Claims.Where(x => x.Type == "username").FirstOrDefault().Value;


                var user = await _userManager.FindByNameAsync(username);

                if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return BadRequest("Invalid access token or refresh token");
                }

                var newAccessToken = CreateToken(principal.Claims.ToList());
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                return new ObjectResult(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    refresh_token = newRefreshToken
                });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null) return BadRequest("Invalid user name");

                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;


        }

        private string GenerateRefreshToken()
        {
            try
            {
                var randomNumber = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}