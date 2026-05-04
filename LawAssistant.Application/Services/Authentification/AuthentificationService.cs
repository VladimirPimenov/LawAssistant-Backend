using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Application.Models.Authentification;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
    public class AuthentificationService(
        IHashService hashService,
        ITokenProvider tokenProvider,
        ILawyerService lawyerService) 
        : IAuthentificationService
    {
        public async Task<RegisterResponce> RegisterAsync(RegisterRequest registerRequest)
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

            return new RegisterResponce
            {
                Id = registeredUser.LawyerId,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                Email = registeredUser.Email
            };
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            var user = await lawyerService.GetLawyerByEmailAsync(loginRequest.Email);

            if (user == null)
                return null;

            if (!hashService.Verify(loginRequest.Password, user.HashedPassword))
                return null;

            var token = tokenProvider.GenerateToken(user);

            return token;
        }

    }
}
