using ChatApplication.Domain.Entities;
using System.Security.Cryptography;

namespace ChatApplication.Service.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(User user);
    }
}
