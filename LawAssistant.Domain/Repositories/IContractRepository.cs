using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	/// <summary>
	/// Репозиторий для работы с коллективными договорами
	/// </summary>
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

		public Task<List<Account>> GetContractAuthorsAsync(CollectiveContract contract);
	}
}
