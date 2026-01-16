using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	public interface IContractService
	{
		public Task<List<CollectiveContract>> GetLawyerContractsAsync(int lawyerId);

		public Task<CollectiveContract> GetContract(int contractId);

		public Task<CollectiveContract> CreateContractAsync(CollectiveContract contract);

		public Task<CollectiveContract> UpdateContractAsync(CollectiveContract updatedContract);

		public Task<int> RemoveContractAsync(int contractId);
	}
}
