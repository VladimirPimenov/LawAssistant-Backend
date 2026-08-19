using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	internal class AccountRepository(
		PostgreSqlDbContext dbContext)
		: IAccountRepository
	{
		public async Task<Account> CreateAccountAsync(Account account)
		{
			dbContext.Account.Add(account);
			await dbContext.SaveChangesAsync();

			return account;
		}

		public async Task<Account> GetAccountByEmailAsync(string email)
		{
			return await dbContext.Account
				.Include(a => a.Role)
				.FirstOrDefaultAsync(a => a.Email == email);
		}

		public async Task<Account> GetAccountAsync(int accountId)
		{
			return await dbContext.Account
				.Include(a => a.Role)
				.FirstOrDefaultAsync(a => a.AccountId == accountId);
		}

		public async Task<Account> UpdateAccountAsync(Account account)
		{
			dbContext.Account.Update(account);
			await dbContext.SaveChangesAsync();

			return account;
		}

		public async Task<List<Account>> GetAllAccountsAsync()
		{
			return await dbContext.Account.ToListAsync();
		}
	}
}
