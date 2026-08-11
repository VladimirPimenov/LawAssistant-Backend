namespace LawAssistant.Domain.Entities
{
	/// <summary>
	/// Абзац коллективного договора
	/// </summary>
	public class ContractParagraph
	{
		/// <summary>
		/// Идентификатор абзаца
		/// </summary>
		public int ParagraphId { get; set; }

		/// <summary>
		/// Идентификатор коллективного договора
		/// </summary>
		public int ContractId { get; set; }

		/// <summary>
		/// Текст абзаца
		/// </summary>
		public string Text { get; set; }
	}
}
