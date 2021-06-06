using System.ComponentModel.DataAnnotations;

namespace RestWebApi.Entities
{
    public class LoginUserRequest
    {
        [Required] 
        public string Email { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}