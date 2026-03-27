using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Models
{
    public record CreateContractRequest
    {
        public string Title { get; init; }

        public IFormFile ContractFile { get; init; }
    }
}
