namespace LawAssistant.Domain.Entities
{
	/// <summary>
	/// Коллективный договор
	/// </summary>
	public class CollectiveContract
	{
		/// <summary>
		/// Идентификатор договора
		/// </summary>
		public int ContractId { get; set; }

		/// <summary>
		/// Название договора
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Дата создания
		/// </summary>
		public DateTime CreatedDate { get; set; }
		
		/// <summary>
		/// Дата последнего изменения
		/// </summary>
		public DateTime ModifiedDate { get; set; }

		/// <summary>
		/// Абзацы договора
		/// </summary>
		public List<ContractParagraph> ContractParagraphs { get; set; }

		/// <summary>
		/// Ключ файла договора в файловом хранилище
		/// </summary>
		public Guid? FileKey { get; set; }
	}
}
