namespace LawAssistant.Domain.Entities
{
	/// <summary>
	/// Представляет аккаунт пользователя
	/// </summary>
	public class Account
	{
		/// <summary>
		/// Идентификатор аккаунта
		/// </summary>
		public int AccountId { get; set;  }

		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName { get; set; }
	
		/// <summary>
		/// Адрес электронной почты
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Хешированный пароль
		/// </summary>
		public string HashedPassword { get; set; }
	}
}
