using ChatApplication.Domain.Entities;
using ChatApplication.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Service.Services
{
    public class UserService : IUserService
    {
        private AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
