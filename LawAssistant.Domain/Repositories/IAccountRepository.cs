using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	/// <summary>
	/// Репозиторий для работы с аккаунтами пользователей
	/// </summary>
	public interface IAccountRepository
	{
		public Task<Account> GetAccountAsync(int accountId);

		public Task<Account> GetAccountByEmailAsync(string email);

		public Task<List<Account>> GetAllAccountsAsync();

		public Task<Account> CreateAccountAsync(Account account);

		public Task<Account> UpdateAccountAsync(Account account);
	}
}
