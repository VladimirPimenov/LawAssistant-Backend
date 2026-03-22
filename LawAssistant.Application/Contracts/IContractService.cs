using Microsoft.AspNetCore.Http;

using LawAssistant.Domain.Entities;
using LawAssistant.Application.Models;

namespace LawAssistant.Application.Contracts
{
	public interface IContractService
	{
		public Task<List<CollectiveContract>> GetLawyerContractsAsync(int lawyerId);

		public Task<CollectiveContract> GetContract(int contractId);

		public Task<CollectiveContract> CreateContractAsync(ContractDto contractDto);

		public Task<CollectiveContract> UpdateContractAsync(CollectiveContract updatedContract);

		public Task<int> RemoveContractAsync(int contractId);
	}
}
