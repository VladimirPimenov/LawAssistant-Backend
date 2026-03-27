using Microsoft.AspNetCore.Http;

using LawAssistant.Domain.Entities;
using LawAssistant.Application.Models;

namespace LawAssistant.Application.Contracts
{
	public interface IContractService
	{
		public Task<List<ContractDto>> GetLawyerContractsInfoAsync(int lawyerId);

		public Task<CollectiveContract> GetContractAsync(int contractId);

		public Task<CollectiveContract> CreateContractAsync(CreateContractRequest contractRequest);

		public Task<CollectiveContract> UpdateContractAsync(CollectiveContract updatedContract);

		public Task<int> RemoveContractAsync(int contractId);
	}
}
