using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Service.Dtos.Users
{
    public class AccountRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
