using ChatApplication.Domain.Entities;

namespace ChatApplication.Service.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(int id);
        public Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
