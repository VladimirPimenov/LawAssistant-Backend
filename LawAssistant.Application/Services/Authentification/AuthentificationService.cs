using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Services
{
    public class AuthentificationService(
        IHashService hashService,
        ITokenProvider tokenProvider,
        ILawyerService lawyerService,
        IHttpContextAccessor httpContextAccessor) 
        : IAuthentificationService
    {
        public async Task<LawyerDto> RegisterAsync(RegisterRequest registerRequest)
        {
            string hashedPassword = hashService.Hash(registerRequest.Password);

            var newUser = new Lawyer
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                HashedPassword = hashedPassword
            };

            var registeredUser = await lawyerService.CreateLawyerAsync(newUser);
            if (registeredUser == null)
                return null;

            return new LawyerDto
            {
                LawyerId = registeredUser.LawyerId,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                Email = registeredUser.Email
            };
        }

        public async Task<LawyerDto> LoginAsync(LoginRequest loginRequest)
        {
            var user = await lawyerService.GetLawyerByEmailAsync(loginRequest.Email);

            if (user == null)
                return null;

            if (!hashService.Verify(loginRequest.Password, user.HashedPassword))
                return null;

            var token = tokenProvider.GenerateToken(user);
            
            var httpContext = httpContextAccessor.HttpContext;
            httpContext?.Response.Cookies.Append("token", token);

            return new LawyerDto
            {
                LawyerId = user.LawyerId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public void Logout()
        {
            var httpContext = httpContextAccessor.HttpContext;
            httpContext?.Response.Cookies.Delete("token");
        }
    }
}
