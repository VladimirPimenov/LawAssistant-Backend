using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Application.Settings;

namespace LawAssistant.Application.Services
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly JwtConfiguration jwtConfiguration;

        public JwtTokenProvider(IConfiguration config)
        {
            jwtConfiguration = config.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();
        }

        public string GenerateToken(Lawyer lawyer)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", lawyer.LawyerId.ToString())
            };

            var key = jwtConfiguration.SecretKey;

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(jwtConfiguration.ExpirationTimeInMinutes));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
