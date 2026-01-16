namespace LawAssistant.Domain.Entities
{
	public class CollectiveContract
	{
		public int ContractId { get; set; }

		public int LawyerId { get; set; }

		public DateOnly CreatedDate { get; set; }
	}
}
