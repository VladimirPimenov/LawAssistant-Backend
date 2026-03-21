using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	public class ContractRepository(
		PostgreSqlDbContext dbContext)
		: IContractRepository
	{
		public async Task<CollectiveContract> GetCollectiveContractAsync(int contractId)
		{
			return await dbContext.CollectiveContract
				.Include(c => c.ContractParagraphs)
				.FirstOrDefaultAsync(c => c.ContractId == contractId);
		}

		public async Task<List<CollectiveContract>> GetLawyerContractsAsync(int lawyerId)
		{
			var lawyerContractIds = await dbContext.LawyerContract
				.Where(lc => lc.LawyerId == lawyerId)
				.Select(lc => lc.ContractId)
				.ToListAsync();

			return await dbContext.CollectiveContract
				.Include(c => c.ContractParagraphs)
				.Where(c => lawyerContractIds.Contains(c.ContractId))
				.ToListAsync();
		}

		public async Task<CollectiveContract> CreateCollectiveContractAsync(CollectiveContract contract)
		{
			dbContext.CollectiveContract.Add(contract);
			await dbContext.SaveChangesAsync();

			return contract;
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
