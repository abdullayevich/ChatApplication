using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthService _authService;

        public AccountService(AppDbContext dbContext, IAuthService authService)
        {
            this._dbContext = dbContext;
            this._authService = authService;
            
        }
        public async Task<string> LoginAsync(AccountLoginDto loginDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            string token = _authService.GenerateToken(user);

            return token;
        }

        public async Task<User> RegisterAsync(AccountRegisterDto registerDto)
        {
            User user = new User()
            {

                Username = registerDto.UserName,
                Email = registerDto.Email,
                Password = registerDto.Password,
                Status = true,
                CreatedAt = DateTime.UtcNow.AddHours(5),
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
