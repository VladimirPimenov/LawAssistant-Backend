namespace LawAssistant.Domain.Entities
{
	/// <summary>
	/// Статья законодательного акта
	/// </summary>
	public class ActArticle
	{
		/// <summary>
		/// Идентификатор статьи
		/// </summary>
		public int ArticleId { get; set; }

		/// <summary>
		/// Идентификатор законодательного акта
		/// </summary>
		public int ActId { get; set; }

		/// <summary>
		/// Номер статьи в акте
		/// </summary>
		public string Number { get; set; }

		/// <summary>
		/// Название статьи
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Текст статьи
		/// </summary>
		public string Text { get; set; }
	}
}
