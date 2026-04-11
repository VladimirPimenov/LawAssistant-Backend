using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	public interface IContractRepository
	{
		public Task<CollectiveContract> GetContractAsync(int contractId);

		public Task<CollectiveContract> GetContractWithParagraphsAsync(int contractId);

		public Task<List<CollectiveContract>> GetLawyerContractsAsync(int lawyerId);

		public Task<CollectiveContract> CreateContractAsync(CollectiveContract contract);

		public Task<CollectiveContract> UpdateContractAsync(CollectiveContract updatedContract);

		public Task<int> RemoveContractAsync(CollectiveContract contract);

		public Task AddAuthorToContractAsync(int lawyerId, int contractId);

		public Task RemoveAuthorFromContractAsync(int lawyerId, int contractId);

		public Task<List<Lawyer>> GetContractAuthorsAsync(CollectiveContract contract);
	}
}
