using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	public interface IContractRepository
	{
		public Task<CollectiveContract> GetCollectiveContractAsync(int contractId);

		public Task<CollectiveContract> CreateCollectiveContractAsync(CollectiveContract contract);

		public Task<CollectiveContract> UpdateContractAsync(CollectiveContract updatedContract);

		public Task<int> RemoveContractAsync(int contractId);
	}
}
