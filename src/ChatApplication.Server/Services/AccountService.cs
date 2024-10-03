using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Users;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;

        public AccountService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            
        }
        // To'ldirilishi kerak !!!
        public async Task<User> LoginAsync(AccountLoginDto loginDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            return user;
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
