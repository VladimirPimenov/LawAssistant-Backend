namespace LawAssistant.Domain.Entities
{
	public class Notification
	{
		public int NotificationId { get; set; }

		public int LawyerId { get; set; }

		public string Text { get; set; }

		public DateTime Date { get; set; }

		public bool IsReaded { get; set;  }
	}
}
