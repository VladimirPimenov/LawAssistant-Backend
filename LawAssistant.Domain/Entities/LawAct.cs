namespace LawAssistant.Domain.Entities
{
	/// <summary>
	/// Законодательный акт
	/// </summary>
	public class LawAct
	{	
		/// <summary>
		/// Идентификатор законодательного акта
		/// </summary>
		public int ActId { get; set; }

		/// <summary>
		/// Название акта
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Дата принятия акта
		/// </summary>
		public DateTime AdoptedDate { get; set; }

		/// <summary>
		/// Статьи законодательного акта
		/// </summary>
		public List<ActArticle> Articles { get; set; }
	}
}
