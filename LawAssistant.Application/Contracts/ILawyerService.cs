using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    public interface ILawyerService
    {
        public Task<List<LawyerDto>> GetLawyersListAsync();

        public Task<Lawyer> GetLawyerByEmailAsync(string email);

        public Task<Lawyer> CreateLawyerAsync(Lawyer lawyer);

        public Task<LawyerDto> UpdateLawyerInfoAsync(LawyerDto lawyerDto);

        public Task<LawyerDto> ChangePasswordAsync(Lawyer lawyer);
    }
}
