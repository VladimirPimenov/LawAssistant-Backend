using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using LawAssistant.Infrastructure.RepositoryImplementation.Models;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	public class ContractRepository(
		PostgreSqlDbContext dbContext)
		: IContractRepository
	{
		public async Task<CollectiveContract> GetContractAsync(int contractId)
		{
			return await dbContext.CollectiveContract.FirstOrDefaultAsync(c => c.ContractId == contractId);
		}

		public async Task<CollectiveContract> GetContractWithParagraphsAsync(int contractId)
		{
			return await dbContext.CollectiveContract
				.Include(c => c.ContractParagraphs)
				.FirstOrDefaultAsync(c => c.ContractId == contractId);
		}

		public async Task<List<CollectiveContract>> GetLawyerContractsAsync(int lawyerId)
		{
			var contractIds = await dbContext.LawyerContract
				.Where(lc => lc.LawyerId == lawyerId)
				.Select(lc => lc.ContractId)
				.ToListAsync();

			return await dbContext.CollectiveContract
				.Where(c => contractIds.Contains(c.ContractId))
				.ToListAsync();
		}

		public async Task<CollectiveContract> CreateContractAsync(CollectiveContract contract)
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
			dbContext.CollectiveContract.Update(updatedContract);
			await dbContext.SaveChangesAsync();

			return updatedContract;
		}

		public async Task AddAuthorToContractAsync(int lawyerId, int contractId)
		{
			var lawyerContract = new LawyerContract
			{
				LawyerId = lawyerId,
				ContractId = contractId
			};

			dbContext.LawyerContract.Add(lawyerContract);
			await dbContext.SaveChangesAsync();
		}

		public async Task RemoveAuthorFromContractAsync(int lawyerId, int contractId)
		{
			var lawyerContract = await dbContext.LawyerContract
									.FirstOrDefaultAsync(lc => 
										lc.LawyerId == lawyerId 
										&& lc.ContractId == contractId);

			dbContext.LawyerContract.Remove(lawyerContract);
			await dbContext.SaveChangesAsync();
		}

		public async Task<List<Lawyer>> GetContractAuthorsAsync(CollectiveContract contract)
		{
			var lawyerIds = await dbContext.LawyerContract
				.Where(lc => lc.ContractId == contract.ContractId)
				.Select(lc => lc.LawyerId)
				.ToListAsync();

			return await dbContext.Lawyer
				.Where(l => lawyerIds.Contains(l.LawyerId))
				.ToListAsync();
		}
	}
}
