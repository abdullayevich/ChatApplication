using ChatApplication.Domain.Entities;
using ChatApplication.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApplication.Service.Services;
public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        this._config = config.GetSection("Jwt");
    }
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("UserName", user.Username),
            new Claim("Email", user.Email)
        };
        var res = _config["SecretKey"];
        //var res = _config.GetConnectionString("SecretKey"); 

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(_config["Issuer"], _config["Audience"], claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_config["Lifetime"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
