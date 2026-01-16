namespace LawAssistant.Domain.Entities
{
	public class ContractParagraph
	{
		public int ParagraphId { get; set; }

		public int ContractId { get; set; }

		public string Text { get; set; }
	}
}
