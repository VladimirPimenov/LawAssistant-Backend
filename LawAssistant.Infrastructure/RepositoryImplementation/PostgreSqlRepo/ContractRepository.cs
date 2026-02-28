using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	public class ContractRepository(
		PostgreSqlDbContext dbContext)
		: IContractRepository
	{
		public async Task<CollectiveContract> CreateCollectiveContractAsync(CollectiveContract contract)
		{
			dbContext.CollectiveContract.Add(contract);
			await dbContext.SaveChangesAsync();

			return contract;
		}

		public async Task<CollectiveContract> GetCollectiveContractAsync(int contractId)
		{
			return await dbContext.CollectiveContract
				.Include(c => c.ContractParagraphs)
				.FirstOrDefaultAsync(c => c.ContractId == contractId);
		}

		public async Task<int> RemoveContractAsync(CollectiveContract contract)
		{
			dbContext.CollectiveContract.Remove(contract);
			await dbContext.SaveChangesAsync();

			return contract.ContractId;
		}

		public async Task<CollectiveContract> UpdateContractAsync(CollectiveContract updatedContract)
		{
			dbContext.CollectiveContract.Attach(updatedContract);
			await dbContext.SaveChangesAsync();

			return updatedContract;
		}
	}
}
