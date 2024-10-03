using ChatApplication.Domain.Entities;
using ChatApplication.Service.Dtos.Users;

namespace ChatApplication.Service.Interfaces
{
    public interface IAccountService
    {
        public Task<User> RegisterAsync(AccountRegisterDto registerDto);

        public Task<User> LoginAsync(AccountLoginDto loginDto);

    }
}
