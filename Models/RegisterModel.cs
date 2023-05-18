using System.ComponentModel.DataAnnotations;

namespace DeadLine.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "IsProfessor is required")]
        public bool IsProfessor { get; set; } 


        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string? Username { get; set; }


        [Required(ErrorMessage = "Mobile Name is required")]
        public string? MobileNumber { get; set; }

        
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
 
    }
}