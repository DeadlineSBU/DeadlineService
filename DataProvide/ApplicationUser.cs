using System;
using Microsoft.AspNetCore.Identity;

namespace DeadLine.DataProvide
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}