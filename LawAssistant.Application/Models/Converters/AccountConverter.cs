using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Models
{
	internal static class LawyerConverter
	{
		public static AccountDto ConvertToDto(this Account account)
		{
			return new AccountDto
			{
				AccountId = account.AccountId,
				FirstName = account.FirstName,
				LastName = account.LastName,
				Email = account.Email
			};
		}
	}
}
