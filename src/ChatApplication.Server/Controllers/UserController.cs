using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ChatApplication.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IAccountService _accountService;

        public UserController(IUserService userService, IAccountService accountService)
        {
            this._userService = userService;
            this._accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUser(AccountRegisterDto registerDto)
        {
            var result = await _accountService.RegisterAsync(registerDto);
            return Ok(result);
        }

        [Route("get_all")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            var res = result.ToList().Select(u => new AllUserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.Username,
                Status = u.Status,
            });
            return Ok(res);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(AccountLoginDto loginDto)
        {
            var token = await _accountService.LoginAsync(loginDto);
            return Ok(token);
        }
    }
}
