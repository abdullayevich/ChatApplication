using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Service.Dtos.Users
{
    public class AccountLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
