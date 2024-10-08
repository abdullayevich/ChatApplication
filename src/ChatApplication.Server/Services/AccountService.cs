using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.Interfaces;
using ChatApplication.Service.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatApplication.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly IMemoryCache _cache;

        public AccountService(AppDbContext dbContext, IAuthService authService, IMemoryCache cache)
        {
            this._dbContext = dbContext;
            this._authService = authService;
            this._cache = cache;
        }
        public async Task<string> LoginAsync(AccountLoginDto loginDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            var hasherResult = PasswordHasher.Verify(loginDto.Password, user.Password);
            if (hasherResult)
            {
                string token = _authService.GenerateToken(user);
                return token;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public async Task<User> RegisterAsync(AccountRegisterDto registerDto)
        {
            var hashresult = PasswordHasher.Hash(registerDto.Password);
            User user = new User()
            {

                Username = registerDto.UserName,
                Email = registerDto.Email,
                Password = hashresult.Hash,
                Status = true,
                CreatedAt = DateTime.UtcNow.AddHours(5),
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
