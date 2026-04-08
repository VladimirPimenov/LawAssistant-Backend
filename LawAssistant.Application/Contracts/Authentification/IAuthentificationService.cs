using LawAssistant.Application.Models.Authentification;

namespace LawAssistant.Application.Contracts
{
    public interface IAuthentificationService
    {
        public Task<RegisterResponce> RegisterAsync(RegisterRequest registerRequest);

        public Task<string> LoginAsync(LoginRequest loginRequest);
    }
}
