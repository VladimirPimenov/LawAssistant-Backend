using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Repositories;

using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Services
{
	internal class LawyerService(
        ILawyerRepository lawyerRepository)
        : ILawyerService
    {
		public async Task<List<LawyerDto>> GetLawyersListAsync()
		{
			var lawyers = await lawyerRepository.GetAllLawyersAsync();

			var dtos = lawyers
				.Select(l => l.ConvertToDto())
				.ToList();
			return dtos;
		}

		public async Task<Lawyer> GetLawyerAsync(int lawyerId)
		{
			return await lawyerRepository.GetLawyerAsync(lawyerId);
		}

		public async Task<Lawyer> GetLawyerByEmailAsync(string email)
		{
			return await lawyerRepository.GetLawyerByEmailAsync(email);
		}

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

		public async Task<Lawyer> CreateLawyerAsync(Lawyer lawyer)
		{
            return await lawyerRepository.CreateLawyerAsync(lawyer);
		}

        public Task<LawyerDto> ChangePasswordAsync(Lawyer lawyer)
        {
            throw new NotImplementedException();
        }
	}
}
