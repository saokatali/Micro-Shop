using System.ComponentModel.DataAnnotations;

namespace Identity.API.Core
{
    public class SignInDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
