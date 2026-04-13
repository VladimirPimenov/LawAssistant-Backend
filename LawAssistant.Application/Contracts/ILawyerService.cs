using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    public interface ILawyerService
    {
        public Task<LawyerDto> UpdateLawyerInfoAsync(LawyerDto lawyerDto);

        public Task<LawyerDto> ChangePasswordAsync(Lawyer lawyer);
    }
}
