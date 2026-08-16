namespace LawAssistant.Domain.Entities
{
	/// <summary>
	/// Уведомление пользователя
	/// </summary>
	public class Notification
	{
		/// <summary>
		/// Идентификатор уведомления
		/// </summary>
		public int NotificationId { get; set; }

		/// <summary>
		/// Идентификатор пользователя, которому предназначено уведомление
		/// </summary>
		public int AccountId { get; set; }

		/// <summary>
		/// Текст уведомления
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Дата уведомления
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Флаг прочтения уведомления
		/// </summary>
		public bool IsReaded { get; set;  }
	}
}
