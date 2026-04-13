using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Repositories;

using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Services
{
    public class LawyerService(
        ILawyerRepository lawyerRepository)
        : ILawyerService
    {
        public async Task<LawyerDto> UpdateLawyerInfoAsync(LawyerDto lawyerDto)
        {
            var lawyer = await lawyerRepository.GetLawyerAsync(lawyerDto.LawyerId);

            if (lawyer == null)
                return null;

            lawyer.FirstName = lawyerDto.FirstName;
            lawyer.LastName = lawyerDto.LastName;

            var updatedLawyer = await lawyerRepository.UpdateLawyerAsync(lawyer);

            return lawyerDto;
        }

        public Task<LawyerDto> ChangePasswordAsync(Lawyer lawyer)
        {
            throw new NotImplementedException();
        }
    }
}
