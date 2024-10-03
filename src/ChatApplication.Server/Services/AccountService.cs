using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Service.Services
{
    public class AccountService : IAccountService
    {
        private AppDbContext _dbContext;

        public AccountService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            
        }
        public Task<User> LoginAsync(AccountLoginDto loginDto)
        {
            throw new NotImplementedException();
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
