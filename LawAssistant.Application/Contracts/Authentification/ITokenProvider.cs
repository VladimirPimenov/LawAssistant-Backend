using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    public interface ITokenProvider
    {
        public string GenerateToken(Lawyer lawyer);
    }
}
