using System;
using Microsoft.AspNetCore.Identity;

namespace DeadLine.DataProvide
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public DateTime LastLogin { get; set; }

        public bool IsProfessor { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


    }
}