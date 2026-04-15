namespace LawAssistant.Domain.Entities
{
	public class CollectiveContract
	{
		public int ContractId { get; set; }

		public string Title { get; set; }

		public DateTime CreatedDate { get; set; }

		public List<ContractParagraph> ContractParagraphs { get; set; }

		public Guid? FileKey { get; set; }
	}
}
