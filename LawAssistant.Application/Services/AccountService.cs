using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Repositories;

using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Services
{
	internal class AccountService(IAccountRepository accountRepository) : IAccountService
    {
		public async Task<Account> GetAccountAsync(int accountId)
		{
			return await accountRepository.GetAccountAsync(accountId);
		}

		public async Task<Account> GetAccountByEmailAsync(string email)
		{
			return await accountRepository.GetAccountByEmailAsync(email);
		}

		public async Task<AccountDto> UpdateAccountInfoAsync(AccountDto accountDto)
        {
            var account = await accountRepository.GetAccountAsync(accountDto.AccountId);

            if (account == null)
                return null;

            account.FirstName = accountDto.FirstName;
            account.LastName = accountDto.LastName;

            var updatedAccount = await accountRepository.UpdateAccountAsync(account);

            return updatedAccount.ConvertToDto();
        }

		public async Task<Account> CreateAccountAsync(Account account)
		{
            return await accountRepository.CreateAccountAsync(account);
		}

        public Task<AccountDto> ChangePasswordAsync(Account account)
        {
            throw new NotImplementedException();
        }
	}
}
