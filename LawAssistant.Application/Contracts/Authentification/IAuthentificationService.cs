using LawAssistant.Application.Models;

namespace LawAssistant.Application.Contracts
{
    public interface IAuthentificationService
    {
        public Task<LawyerDto> RegisterAsync(RegisterRequest registerRequest);

        public Task<string> LoginAsync(LoginRequest loginRequest);
    }
}
