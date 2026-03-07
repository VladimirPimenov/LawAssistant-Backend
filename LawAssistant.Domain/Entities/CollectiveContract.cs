namespace LawAssistant.Domain.Entities
{
	public class CollectiveContract
	{
		public int ContractId { get; set; }

		public DateTime CreatedDate { get; set; }

		public List<ContractParagraph> ContractParagraphs { get; set; }
	}
}
