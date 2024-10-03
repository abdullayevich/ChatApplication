using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(result);
        }
    }
}
