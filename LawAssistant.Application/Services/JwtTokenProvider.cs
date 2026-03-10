using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Services
{
    public class JwtTokenProvider(
        IConfiguration config)
        : ITokenProvider
    {
        public string GenerateToken(Lawyer lawyer)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", lawyer.LawyerId.ToString())
            };

            var key = config.GetValue<string>("JwtConfiguration:Secretkey");

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(2));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
